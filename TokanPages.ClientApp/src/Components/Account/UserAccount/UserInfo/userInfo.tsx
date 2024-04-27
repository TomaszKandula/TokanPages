import * as React from "react";
import { useHistory } from "react-router";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../../../Store/Configuration";
import {
    ApplicationDialogAction,
    UserUpdateAction,
    UserDataStoreAction,
    UserSigninAction,
} from "../../../../Store/Actions";
import { ExecuteAsync, GetConfiguration, NOTIFICATION_STATUS, RequestContract } from "../../../../Api/Request";
import { NotificationData, UserActivationData } from "../../../../Api/Models";
import { useInterval } from "../../../../Shared/Hooks";
import { useWebSockets } from "../../../../Shared/Services/WebSockets";
import { GetTextWarning, SuccessMessage, WarningMessage } from "../../../../Shared/Services/Utilities";
import { AccountFormInput, ValidateAccountForm } from "../../../../Shared/Services/FormValidation";
import { RECEIVED_ERROR_MESSAGE, SET_INTERVAL_DELAY } from "../../../../Shared/constants";
import { OperationStatus } from "../../../../Shared/enums";
import { ReactChangeEvent, ReactKeyboardEvent } from "../../../../Shared/types";
import { UserInfoView } from "./View/userInfoView";
import Validate from "validate.js";

interface UpdateStoreProps {
    canUpdate: boolean;
    isVerified: boolean;
}

export const UserInfo = (): JSX.Element => {
    const dispatch = useDispatch();
    const history = useHistory();
    const socket = useWebSockets();

    const template = useSelector((state: ApplicationState) => state.contentTemplates?.content);
    const account = useSelector((state: ApplicationState) => state.contentAccount);
    const store = useSelector((state: ApplicationState) => state.userDataStore.userData);
    const update = useSelector((state: ApplicationState) => state.userUpdate);
    const media = useSelector((state: ApplicationState) => state.userMediaUpload);
    const error = useSelector((state: ApplicationState) => state.applicationError);

    const hasUpdateNotStarted = update?.status === OperationStatus.notStarted;
    const hasUpdateFinished = update?.status === OperationStatus.hasFinished;
    const hasMediaUploadFinished = media?.status === OperationStatus.hasFinished;
    const hasError = error?.errorMessage === RECEIVED_ERROR_MESSAGE;

    const formDefault: AccountFormInput = {
        ...store,
        userAboutText: store.shortBio ?? "",
    };

    const avatarName = Validate.isEmpty(store.avatarName) ? "N/A" : store.avatarName;
    const [isUserActivated, setIsUserActivated] = React.useState({ checked: true });
    const [canCheckAltStatus, _] = React.useState(false);
    const [form, setForm] = React.useState(formDefault);
    const [isRequesting, setRequesting] = React.useState(false);
    const [hasProgress, setHasProgress] = React.useState(false);
    const [canUpdateStore, setUpdateStore] = React.useState<UpdateStoreProps | undefined>(undefined);

    const showSuccess = (text: string) =>
        dispatch(ApplicationDialogAction.raise(SuccessMessage(template.forms.textAccountSettings, text)));
    const showWarning = (text: string) =>
        dispatch(ApplicationDialogAction.raise(WarningMessage(template.forms.textAccountSettings, text)));

    const clear = React.useCallback(() => {
        if (!hasProgress) return;

        dispatch(UserUpdateAction.clear());
        setHasProgress(false);

        if (!isUserActivated.checked) {
            dispatch(UserSigninAction.clear());
            dispatch(UserDataStoreAction.clear());
            history.push("/");
        }
    }, [hasProgress]);

    const keyHandler = React.useCallback(
        (event: ReactKeyboardEvent) => {
            if (event.code === "Enter") {
                saveButtonHandler();
            }
        },
        [form.email, form.firstName, form.lastName]
    );

    const formHandler = React.useCallback(
        (event: ReactChangeEvent) => {
            setForm({ ...form, [event.currentTarget.name]: event.currentTarget.value });
        },
        [form]
    );

    const switchHandler = React.useCallback(
        (event: ReactChangeEvent) => {
            setIsUserActivated({ ...isUserActivated, [event.target.name]: event.target.checked });
        },
        [isUserActivated]
    );

    const saveButtonHandler = React.useCallback(() => {
        const result = ValidateAccountForm(form);
        if (!Validate.isDefined(result)) {
            setHasProgress(true);
            return;
        }

        showWarning(GetTextWarning({ object: result, template: template.templates.user.updateWarning }));
    }, [form]);

    const verifyButtonHandler = React.useCallback(() => {
        setRequesting(true);
    }, [isRequesting]);

    React.useEffect(() => {
        if (hasError) {
            clear();
            return;
        }

        if (hasUpdateNotStarted && hasProgress) {
            dispatch(
                UserUpdateAction.update({
                    id: store.userId,
                    isActivated: isUserActivated.checked,
                    firstName: form.firstName,
                    lastName: form.lastName,
                    emailAddress: form.email,
                    userAboutText: form.userAboutText,
                })
            );

            return;
        }

        if (hasUpdateFinished) {
            dispatch(
                UserDataStoreAction.update({
                    ...store,
                    firstName: form.firstName,
                    lastName: form.lastName,
                    email: form.email,
                    shortBio: form.userAboutText,
                })
            );

            showSuccess(
                isUserActivated.checked ? template.templates.user.updateSuccess : template.templates.user.deactivation
            );

            clear();
        }
    }, [
        store,
        template,
        form.firstName,
        form.lastName,
        form.email,
        form.userAboutText,
        isUserActivated.checked,
        hasProgress,
        hasError,
        hasUpdateNotStarted,
        hasUpdateFinished,
    ]);

    React.useEffect(() => {
        if (hasMediaUploadFinished) {
            if (media.handle === undefined) return;
            if (media.payload?.blobName === undefined) return;

            const blobName = media.payload?.blobName;
            dispatch(UserDataStoreAction.update({ ...store, avatarName: blobName }));
        }
    }, [store, media.handle, media.payload?.blobName, hasMediaUploadFinished]);

    // React.useEffect(() => {
    //     if (hasError) {
    //         return;
    //     }

    //     if (hasVerificationNotStarted && isRequesting) {
    //         dispatch(
    //             UserEmailVerificationAction.verify({
    //                 emailAddress: form.emailAddress,
    //             })
    //         );

    //         return;
    //     }

    //     if (hasVerificationFinished) {
    //         setRequesting(false);
    //         setCheckAltStatus(true);
    //         dispatch(UserEmailVerificationAction.clear());
    //         showSuccess(template.templates.user.emailVerification);
    //     }
    // }, [hasError, isRequesting, template, form.email, hasVerificationNotStarted, hasVerificationFinished]);

    React.useEffect(() => {
        if (canUpdateStore?.canUpdate) {
            dispatch(UserDataStoreAction.update({ ...store, isVerified: canUpdateStore.isVerified }));
            setUpdateStore(undefined);
        }
    }, [store, canUpdateStore?.canUpdate]);

    React.useEffect(() => {
        socket.startConnection()?.then(() => {
            socket.registerHandler("user_activated", (notification: string) => {
                const data = JSON.parse(notification) as NotificationData;
                const payload = data.payload as UserActivationData;
                setUpdateStore({ canUpdate: true, isVerified: payload.isVerified });
            });
        });
    }, []);

    React.useEffect(() => {
        return () => {
            socket.stopConnection();
        };
    }, []);

    // Check notification status via HTTP in case WebSockets fails.
    useInterval(async () => {
        if (store.isVerified) {
            return;
        }

        if (!canCheckAltStatus) {
            return;
        }

        if (Validate.isEmpty(store.userId)) {
            return;
        }

        const request: RequestContract = {
            configuration: {
                url: NOTIFICATION_STATUS,
                method: "POST",
                responseType: "json",
                data: { statusId: store.userId },
            },
        };

        const response = await ExecuteAsync(GetConfiguration(request));
        const notification = response.content as NotificationData;

        if (notification.handler === "user_activated") {
            const payload = notification.payload as UserActivationData;
            setUpdateStore({ canUpdate: true, isVerified: payload.isVerified });
        }
    }, SET_INTERVAL_DELAY);

    return (
        <UserInfoView
            isLoading={account.isLoading}
            userStore={store}
            accountForm={form}
            userImageName={avatarName}
            isUserActivated={isUserActivated.checked}
            formProgress={hasProgress}
            keyHandler={keyHandler}
            formHandler={formHandler}
            switchHandler={switchHandler}
            buttonHandler={saveButtonHandler}
            verifyButtonHandler={verifyButtonHandler}
            sectionAccessDenied={account.content?.sectionAccessDenied}
            sectionAccountInformation={account.content?.sectionAccountInformation}
        />
    );
};
