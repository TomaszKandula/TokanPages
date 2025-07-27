import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../../../Store/Configuration";
import {
    ApplicationDialogAction,
    UserUpdateAction,
    UserDataStoreAction,
    UserEmailVerificationAction,
} from "../../../../Store/Actions";
import { ExecuteApiActionProps, NOTIFICATION_STATUS } from "../../../../Api";
import { NotificationData, UserActivationData } from "../../../../Api/Models";
import { useApiAction, useDimensions, useInterval } from "../../../../Shared/Hooks";
import { useWebSockets } from "../../../../Shared/Services/WebSockets";
import { AccountFormInput, ValidateAccountForm } from "../../../../Shared/Services/FormValidation";
import { RECEIVED_ERROR_MESSAGE, SET_INTERVAL_DELAY } from "../../../../Shared/constants";
import { IconType, OperationStatus } from "../../../../Shared/enums";
import { ReactChangeEvent, ReactChangeTextEvent, ReactKeyboardEvent } from "../../../../Shared/types";
import { UserInfoView } from "./View/userInfoView";
import Validate from "validate.js";

interface UpdateStoreProps {
    canUpdate: boolean;
    isVerified: boolean;
}

export interface UserInfoProps {
    className?: string;
}

export const UserInfo = (props: UserInfoProps): React.ReactElement => {
    const dispatch = useDispatch();
    const actions = useApiAction();
    const socket = useWebSockets();
    const mediaQuery = useDimensions();

    const store = useSelector((state: ApplicationState) => state.userDataStore.userData);
    const update = useSelector((state: ApplicationState) => state.userUpdate);
    const media = useSelector((state: ApplicationState) => state.userMediaUpload);
    const error = useSelector((state: ApplicationState) => state.applicationError);
    const verification = useSelector((state: ApplicationState) => state.userEmailVerification);
    const contentPageData = useSelector((state: ApplicationState) => state.contentPageData);
    const template = contentPageData.components.templates;
    const account = contentPageData.components.accountSettings;

    const hasUpdateNotStarted = update?.status === OperationStatus.notStarted;
    const hasUpdateFinished = update?.status === OperationStatus.hasFinished;
    const hasMediaUploadFinished = media?.status === OperationStatus.hasFinished;
    const hasVerificationNotStarted = verification?.status === OperationStatus.notStarted;
    const hasVerificationFinished = verification?.status === OperationStatus.hasFinished;
    const hasError = error?.errorMessage === RECEIVED_ERROR_MESSAGE;

    const formDefault: AccountFormInput = {
        ...store,
        description: store.shortBio ?? "",
    };

    const avatarName = Validate.isEmpty(store.avatarName) ? "N/A" : store.avatarName;
    const [canCheckAltStatus, setCheckAltStatus] = React.useState(false);
    const [form, setForm] = React.useState(formDefault);
    const [isRequesting, setRequesting] = React.useState(false);
    const [hasProgress, setHasProgress] = React.useState(false);
    const [canUpdateStore, setUpdateStore] = React.useState<UpdateStoreProps | undefined>(undefined);

    const clear = React.useCallback(() => {
        if (!hasProgress) return;

        dispatch(UserUpdateAction.clear());
        setHasProgress(false);
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

    const descriptionHandler = React.useCallback(
        (event: ReactChangeTextEvent) => {
            setForm({ ...form, [event.currentTarget.name]: event.currentTarget.value });
        },
        [form]
    );

    const saveButtonHandler = React.useCallback(() => {
        const result = ValidateAccountForm(form);
        if (!Validate.isDefined(result)) {
            setHasProgress(true);
            return;
        }

        dispatch(
            ApplicationDialogAction.raise({
                title: template.forms.textAccountSettings,
                message: template.templates.user.updateWarning,
                validation: result,
                icon: IconType.warning,
            })
        );
    }, [form, template.forms.textAccountSettings, template.templates.user.updateWarning]);

    const verifyButtonHandler = React.useCallback(() => {
        setRequesting(true);
    }, [isRequesting]);

    /* UPDATE USER DATA */
    React.useEffect(() => {
        if (!hasProgress) {
            return;
        }

        if (hasError) {
            clear();
            return;
        }

        if (hasUpdateNotStarted) {
            dispatch(
                UserUpdateAction.update({
                    id: store.userId,
                    isActivated: true,
                    firstName: form.firstName,
                    lastName: form.lastName,
                    emailAddress: form.email,
                    description: form.description,
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
                    shortBio: form.description,
                    isVerified: !update.response.shouldVerifyEmail,
                })
            );

            dispatch(
                ApplicationDialogAction.raise({
                    title: template.forms.textAccountSettings,
                    message: template.templates.user.updateSuccess,
                    icon: IconType.info,
                })
            );

            clear();
        }
    }, [
        store,
        update.response.shouldVerifyEmail,
        template.forms.textAccountSettings,
        template.templates.user.updateSuccess,
        form.firstName,
        form.lastName,
        form.email,
        form.description,
        hasProgress,
        hasError,
        hasUpdateNotStarted,
        hasUpdateFinished,
    ]);

    /* UPLOAD USER AVATAR */
    React.useEffect(() => {
        if (hasMediaUploadFinished) {
            if (media.handle === undefined) return;
            if (media.payload?.blobName === undefined) return;

            const blobName = media.payload?.blobName;
            dispatch(UserDataStoreAction.update({ ...store, avatarName: blobName }));
        }
    }, [store, media.handle, media.payload?.blobName, hasMediaUploadFinished]);

    /* REQUEST EMAIL VERIFICATION */
    React.useEffect(() => {
        if (hasError) {
            return;
        }

        if (hasVerificationNotStarted && isRequesting) {
            dispatch(
                UserEmailVerificationAction.verify({
                    emailAddress: form.email,
                })
            );

            return;
        }

        if (hasVerificationFinished) {
            setRequesting(false);
            setCheckAltStatus(true);
            dispatch(UserEmailVerificationAction.clear());
            dispatch(
                ApplicationDialogAction.raise({
                    title: template.forms.textAccountSettings,
                    message: template.templates.user.emailVerification,
                    icon: IconType.info,
                })
            );
        }
    }, [hasError, isRequesting, template, form.email, hasVerificationNotStarted, hasVerificationFinished]);

    /*  */
    React.useEffect(() => {
        if (canUpdateStore?.canUpdate) {
            dispatch(UserDataStoreAction.update({ ...store, isVerified: canUpdateStore.isVerified }));
            setUpdateStore(undefined);
        }
    }, [store, canUpdateStore?.canUpdate, canUpdateStore?.isVerified]);

    /* START WEBSOCKETS */
    React.useEffect(() => {
        socket.startConnection()?.then(() => {
            socket.registerHandler("user_activated", (notification: string) => {
                const data = JSON.parse(notification) as NotificationData;
                const payload = data.payload as UserActivationData;
                setUpdateStore({ canUpdate: true, isVerified: payload.isVerified });
            });
        });
    }, []);

    /* STOP WEBSOCKETS */
    React.useEffect(() => {
        return () => {
            socket.stopConnection();
        };
    }, []);

    /* HTTP POLLING IN CASE WEBSOCKETS FAILS */
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

        const request: ExecuteApiActionProps = {
            url: NOTIFICATION_STATUS,
            configuration: {
                method: "POST",
                hasJsonResponse: true,
                body: { statusId: store.userId },
            },
        };

        const response = await actions.apiAction(request);
        const notification = response.content as NotificationData;

        if (notification.handler === "user_activated") {
            const payload = notification.payload as UserActivationData;
            setUpdateStore({ canUpdate: true, isVerified: payload.isVerified });
        }
    }, SET_INTERVAL_DELAY);

    return (
        <UserInfoView
            isLoading={contentPageData.isLoading}
            isMobile={mediaQuery.isMobile}
            userStore={store}
            accountForm={form}
            userImageName={avatarName}
            isRequestingVerification={isRequesting}
            formProgress={hasProgress}
            keyHandler={keyHandler}
            formHandler={formHandler}
            descriptionHandler={descriptionHandler}
            saveButtonHandler={saveButtonHandler}
            verifyButtonHandler={verifyButtonHandler}
            sectionAccountInformation={account.sectionAccountInformation}
            description={{
                minRows: 6,
                message: form.description,
            }}
            className={props.className}
        />
    );
};
