import * as React from "react";
import { useHistory } from "react-router";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../../../Store/Configuration";
import { ContentAccountState } from "../../../../Store/States";
import { OperationStatus } from "../../../../Shared/enums";
import { ReactChangeEvent, ReactKeyboardEvent } from "../../../../Shared/types";
import { UserInfoView } from "./View/userInfoView";
import Validate from "validate.js";

import { 
    ApplicationDialogAction, 
    UserUpdateAction, 
    UserDataStoreAction, 
    UserSigninAction
} from "../../../../Store/Actions";

import { 
    GetTextWarning, 
    SuccessMessage, 
    WarningMessage 
} from "../../../../Shared/Services/Utilities";

import { 
    AccountFormInput, 
    ValidateAccountForm, 
} from "../../../../Shared/Services/FormValidation";

import { 
    ACCOUNT_FORM, 
    DEACTIVATE_USER, 
    RECEIVED_ERROR_MESSAGE, 
    UPDATE_USER_SUCCESS, 
    UPDATE_USER_WARNING 
} from "../../../../Shared/constants";

export const UserInfo = (props: ContentAccountState): JSX.Element => 
{
    const dispatch = useDispatch();
    const history = useHistory();

    const store = useSelector((state: ApplicationState) => state.userDataStore.userData);
    const update = useSelector((state: ApplicationState) => state.userUpdate);
    const media = useSelector((state: ApplicationState) => state.userMediaUpload);
    const error = useSelector((state: ApplicationState) => state.applicationError);

    const hasUpdateNotStarted = update?.status === OperationStatus.notStarted;
    const hasUpdateFinished = update?.status === OperationStatus.hasFinished;
    const hasMediaUploadFinished = media?.status === OperationStatus.hasFinished;
    const hasError = error?.errorMessage === RECEIVED_ERROR_MESSAGE;

    const formDefault: AccountFormInput = 
    {
        ...store,
        userAboutText: store.shortBio ?? ""
    }

    const avatarName = Validate.isEmpty(store.avatarName) ? "N/A" : store.avatarName;
    const [isUserActivated, setIsUserActivated] = React.useState({ checked: true });
    const [form, setForm] = React.useState(formDefault);
    const [hasProgress, setHasProgress] = React.useState(false);

    const showSuccess = (text: string) => dispatch(ApplicationDialogAction.raise(SuccessMessage(ACCOUNT_FORM, text)));
    const showWarning = (text: string)=> dispatch(ApplicationDialogAction.raise(WarningMessage(ACCOUNT_FORM, text)));

    const clear = React.useCallback(() => 
    {
        if (!hasProgress) return;
        
        dispatch(UserUpdateAction.clear());
        setHasProgress(false);

        if (!isUserActivated.checked)
        {
            dispatch(UserSigninAction.clear());
            dispatch(UserDataStoreAction.clear());
            history.push("/");
        }
    }, 
    [ hasProgress ]);

    React.useEffect(() => 
    {
        if (hasError)
        {
            clear();
            return;
        }

        if (hasUpdateNotStarted && hasProgress) 
        {
            dispatch(UserUpdateAction.update(
            {
                id: store.userId,
                isActivated: isUserActivated.checked,
                firstName: form.firstName,
                lastName: form.lastName,
                emailAddress: form.email,
                userAboutText: form.userAboutText
            }));

            return;
        }

        if (hasUpdateFinished)
        {
            dispatch(UserDataStoreAction.update({ 
                ...store,  
                firstName: form.firstName,
                lastName: form.lastName,
                email: form.email,
                shortBio: form.userAboutText
            }));

            showSuccess(isUserActivated.checked ? UPDATE_USER_SUCCESS : DEACTIVATE_USER);
            clear();
        }
    }, 
    [ 
        hasProgress, 
        hasError, 
        hasUpdateNotStarted, 
        hasUpdateFinished 
    ]);

    React.useEffect(() => 
    {
        if (hasMediaUploadFinished)
        {
            if (media.handle === undefined) return;
            if (media.payload?.blobName === undefined) return;

            const blobName = media.payload?.blobName;
            dispatch(UserDataStoreAction.update({ ...store, avatarName: blobName }));
        }
    }, 
    [ hasMediaUploadFinished ]);

    const accountKeyHandler = React.useCallback((event: ReactKeyboardEvent) => 
    {
        if (event.code === "Enter")
        {
            buttonHandler();
        }
    }, [
        form.email, 
        form.firstName, 
        form.lastName
    ]);

    const formHandler = React.useCallback((event: ReactChangeEvent) => 
    {
        setForm({ ...form, [event.currentTarget.name]: event.currentTarget.value }); 
    }, 
    [ form ]);

    const switchHandler = React.useCallback((event: ReactChangeEvent) => 
    {
        setIsUserActivated({ ...isUserActivated, [event.target.name]: event.target.checked });
    }, 
    [ isUserActivated ]);

    const buttonHandler = React.useCallback(() => 
    {
        const result = ValidateAccountForm(form);
        if (!Validate.isDefined(result))
        {
            setHasProgress(true);
            return;
        }

        showWarning(GetTextWarning({ object: result, template: UPDATE_USER_WARNING }));
    }, 
    [ form ]);

    return(
        <UserInfoView
            isLoading={props.isLoading}
            userStore={store}
            accountForm={form}
            userImageName={avatarName}
            isUserActivated={isUserActivated.checked}
            formProgress={hasProgress}
            keyHandler={accountKeyHandler}
            formHandler={formHandler}
            switchHandler={switchHandler}
            buttonHandler={buttonHandler}
            sectionAccessDenied={props.content?.sectionAccessDenied}
            sectionAccountInformation={props.content?.sectionAccountInformation}
        />
    );
}
