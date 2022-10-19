import * as React from "react";
import { useHistory } from "react-router";
import { useDispatch, useSelector } from "react-redux";
import { IApplicationState } from "../../../../Store/Configuration";
import { IContentAccount } from "../../../../Store/States";
import { OperationStatus } from "../../../../Shared/enums";
import { UserInfoView } from "./View/userInfoView";
import Validate from "validate.js";

import { 
    ApplicationDialogAction, 
    UserUpdateAction, 
    UserDataStoreAction, 
    UserSigninAction, 
    UserReAuthenticateAction 
} from "../../../../Store/Actions";

import { 
    GetTextWarning, 
    SuccessMessage, 
    WarningMessage 
} from "../../../../Shared/Services/Utilities";

import { 
    IValidateAccountForm, 
    ValidateAccountForm, 
} from "../../../../Shared/Services/FormValidation";

import { 
    ACCOUNT_FORM, 
    DEACTIVATE_USER, 
    RECEIVED_ERROR_MESSAGE, 
    UPDATE_USER_SUCCESS, 
    UPDATE_USER_WARNING 
} from "../../../../Shared/constants";

export const UserInfo = (props: IContentAccount): JSX.Element => 
{
    const dispatch = useDispatch();
    const history = useHistory();

    const userStore = useSelector((state: IApplicationState) => state.userDataStore.userData);
    const appState = useSelector((state: IApplicationState) => state.userUpdate);
    const appError = useSelector((state: IApplicationState) => state.applicationError);

    const accountFormDefault: IValidateAccountForm = 
    {
        ...userStore,
        userAboutText: userStore.shortBio ?? ""
    }

    const [isUserActivated, setIsUserActivated] = React.useState({ checked: true });
    const [accountForm, setAccountForm] = React.useState(accountFormDefault);
    const [accountFormProgress, setAccountFormProgress] = React.useState(false);

    const showSuccess = (text: string) => dispatch(ApplicationDialogAction.raise(SuccessMessage(ACCOUNT_FORM, text)));
    const showWarning = (text: string)=> dispatch(ApplicationDialogAction.raise(WarningMessage(ACCOUNT_FORM, text)));

    const clear = React.useCallback(() => 
    {
        if (!accountFormProgress) return;
        
        dispatch(UserUpdateAction.clear());
        setAccountFormProgress(false);

        if (!isUserActivated.checked)
        {
            dispatch(UserSigninAction.clear());
            dispatch(UserDataStoreAction.clear());
            history.push("/");
        }
        else
        {
            dispatch(UserReAuthenticateAction.reAuthenticate());
        }
    }, 
    [ accountFormProgress ]);

    React.useEffect(() => 
    {
        if (appError?.defaultErrorMessage === RECEIVED_ERROR_MESSAGE)
        {
            clear();
            return;
        }

        switch(appState?.status)
        {
            case OperationStatus.notStarted:
                if (accountFormProgress) dispatch(UserUpdateAction.update(
                {
                    id: userStore.userId,
                    isActivated: isUserActivated.checked,
                    firstName: accountForm.firstName,
                    lastName: accountForm.lastName,
                    emailAddress: accountForm.email,
                    userAboutText: accountForm.userAboutText
                }));
            break;

            case OperationStatus.hasFinished:
                clear();
                showSuccess(isUserActivated.checked ? UPDATE_USER_SUCCESS : DEACTIVATE_USER);
            break;
        }
    }, 
    [ accountFormProgress, appError?.defaultErrorMessage, appState?.status, 
    OperationStatus.notStarted, OperationStatus.hasFinished ]);

    const accountFormHandler = (event: React.ChangeEvent<HTMLInputElement>) => 
    {
        setAccountForm({ ...accountForm, [event.currentTarget.name]: event.currentTarget.value }); 
    };

    const accountSwitchHandler = (event: React.ChangeEvent<HTMLInputElement>) => 
    {
        setIsUserActivated({ ...isUserActivated, [event.target.name]: event.target.checked });
    };

    const accountButtonHandler = () => 
    {
        let validationResult = ValidateAccountForm(accountForm);
        if (!Validate.isDefined(validationResult))
        {
            setAccountFormProgress(true);
            return;
        }

        showWarning(GetTextWarning({ object: validationResult, template: UPDATE_USER_WARNING }));
    };

    return(
        <UserInfoView bind=
        {{
            isLoading: props.isLoading,
            userStore: userStore,
            accountForm: accountForm,
            isUserActivated: isUserActivated.checked,
            accountFormProgress: accountFormProgress,           
            accountFormHandler: accountFormHandler,
            accountSwitchHandler: accountSwitchHandler,
            accountButtonHandler: accountButtonHandler,
            avatarUploadProgress: false,
            avatarButtonHandler: null,
            sectionAccessDenied: props.content?.sectionAccessDenied,
            sectionAccountInformation: props.content?.sectionAccountInformation,
        }} />
    );
}
