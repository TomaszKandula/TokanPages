import * as React from "react";
import { useHistory } from "react-router";
import { useDispatch, useSelector } from "react-redux";
import { ActionCreators as RaiseDialog } from "../../Redux/Actions/raiseDialogAction";
import { ActionCreators as UpdateUser } from "../../Redux/Actions/Users/updateUserAction";
import { ActionCreators as DataAction } from "../../Redux/Actions/Users/storeUserDataAction";
import { ActionCreators as UserAction } from "../../Redux/Actions/Users/signinUserAction";
import { ActionCreators as PasswordAction } from "../../Redux/Actions/Users/updateUserPasswordAction";
import { ActionCreators as ReAuthenticateUser } from "../../Redux/Actions/Users/reAuthenticateUserAction";
import { IApplicationState } from "../../Redux/applicationState";
import { IGetAccountContent } from "../../Redux/States/Content/getAccountContentState";
import { IValidateAccountForm, IValidatePasswordForm, ValidateAccountForm, ValidatePasswordForm } from "../../Shared/Services/FormValidation";
import { ACCOUNT_FORM, DEACTIVATE_USER, RECEIVED_ERROR_MESSAGE, UPDATE_PASSWORD_SUCCESS, UPDATE_USER_SUCCESS, UPDATE_USER_WARNING } from "../../Shared/constants";
import SuccessMessage from "../../Shared/Components/ApplicationDialogBox/Helpers/successMessage";
import WarningMessage from "../../Shared/Components/ApplicationDialogBox/Helpers/warningMessage";
import { GetTextWarning } from "../../Shared/Services/Utilities";
import { OperationStatus } from "../../Shared/enums";
import { IUpdateUserDto, IUpdateUserPasswordDto } from "../../Api/Models";
import UserAccountView from "./userAccountView";
import Validate from "validate.js";

const UserAccount = (props: IGetAccountContent): JSX.Element => 
{
    const dispatch = useDispatch();
    const history = useHistory();
    
    const userDataState = useSelector((state: IApplicationState) => state.storeUserData.userData);
    const updatePasswordState = useSelector((state: IApplicationState) => state.updateUserPassword);
    const updateUserState = useSelector((state: IApplicationState) => state.updateUser);
    const raiseErrorState = useSelector((state: IApplicationState) => state.raiseError);
    const isAnonymous = Validate.isEmpty(userDataState.userId);

    const accountFormDefault: IValidateAccountForm = 
    {
        firstName: userDataState.firstName,
        lastName: userDataState.lastName,
        email: userDataState.email,
        shortBio: userDataState.shortBio ?? ""
    }

    const passwordFormDefault: IValidatePasswordForm = 
    {
        oldPassword: "",
        newPassword: "",
        confirmPassword: ""
    }

    const [isUserActivated, setIsUserActivated] = React.useState({ checked: true });
    const [accountForm, setAccountForm] = React.useState(accountFormDefault);
    const [accountFormProgress, setAccountFormProgress] = React.useState(false);
    const [passwordForm, setPasswordForm] = React.useState(passwordFormDefault);
    const [passwordFormProgress, setPasswordFormProgress] = React.useState(false);

    const showSuccess = React.useCallback((text: string) => dispatch(RaiseDialog.raiseDialog(SuccessMessage(ACCOUNT_FORM, text))), [ dispatch ]);
    const showWarning = React.useCallback((text: string)=> dispatch(RaiseDialog.raiseDialog(WarningMessage(ACCOUNT_FORM, text))), [ dispatch ]);

    const postUpdateUser = React.useCallback((payload: IUpdateUserDto) => dispatch(UpdateUser.update(payload)), [ dispatch ]);
    const postUpdateUserClear = React.useCallback(() => dispatch(UpdateUser.clear()), [ dispatch ]);

    const postUpdatePassword = React.useCallback((payload: IUpdateUserPasswordDto) => dispatch(PasswordAction.update(payload)), [ dispatch ]);
    const postUpdatePasswordClear = React.useCallback(() => dispatch(PasswordAction.clear()), [ dispatch ]);

    const reAuthenticate = React.useCallback(() => dispatch(ReAuthenticateUser.reAuthenticate()), [ dispatch ]);
    const clearLoggedUser = React.useCallback(() => dispatch(UserAction.clear()), [ dispatch ]);
    const clearLoggedData = React.useCallback(() => dispatch(DataAction.clear()), [ dispatch ]);

    const accountFormClear = React.useCallback(() => 
    {
        if (!accountFormProgress) return;
        
        postUpdateUserClear();
        setAccountFormProgress(false);

        if (!isUserActivated.checked)
        {
            clearLoggedUser();
            clearLoggedData();
            history.push("/");
        }
        else
        {
            reAuthenticate();
        }
    }, 
    [ accountFormProgress, postUpdateUserClear, reAuthenticate, clearLoggedUser, clearLoggedData ]);

    const passwordFormClear = React.useCallback(() => 
    {
        if (!passwordFormProgress) return;

        postUpdatePasswordClear();
        setPasswordForm(passwordFormDefault);
        setPasswordFormProgress(false);
    }, 
    [ passwordFormProgress, postUpdatePasswordClear ]);

    React.useEffect(() => 
    {
        if (raiseErrorState?.defaultErrorMessage === RECEIVED_ERROR_MESSAGE)
        {
            accountFormClear();
            return;
        }

        switch(updateUserState?.operationStatus)
        {
            case OperationStatus.notStarted:
                if (accountFormProgress) postUpdateUser(
                {
                    id: userDataState.userId,
                    isActivated: isUserActivated.checked,
                    firstName: accountForm.firstName,
                    lastName: accountForm.lastName,
                    emailAddress: accountForm.email,
                    shortBio: accountForm.shortBio
                });
            break;

            case OperationStatus.hasFinished:
                accountFormClear();
                showSuccess(isUserActivated.checked ? UPDATE_USER_SUCCESS : DEACTIVATE_USER);
            break;
        }
    }, 
    [ accountFormProgress, raiseErrorState?.defaultErrorMessage, updateUserState?.operationStatus, 
        OperationStatus.notStarted, OperationStatus.hasFinished ]);

    React.useEffect(() => 
    {
        if (raiseErrorState?.defaultErrorMessage === RECEIVED_ERROR_MESSAGE)
        {
            passwordFormClear();
            return;
        }

        switch(updatePasswordState?.operationStatus)
        {
            case OperationStatus.notStarted:
                if (passwordFormProgress) postUpdatePassword(
                {
                    oldPassword: passwordForm.oldPassword,
                    newPassword: passwordForm.newPassword
                });
            break;

            case OperationStatus.hasFinished:
                passwordFormClear();
                showSuccess(UPDATE_PASSWORD_SUCCESS);
            break;
        }
    }, 
    [ passwordFormProgress, raiseErrorState?.defaultErrorMessage, updatePasswordState?.operationStatus, 
        OperationStatus.notStarted, OperationStatus.hasFinished ]);

    const accountFormHandler = (event: React.ChangeEvent<HTMLInputElement>) => 
    {
        setAccountForm({ ...accountForm, [event.currentTarget.name]: event.currentTarget.value }); 
    };

    const accountSwitchHandler = (event: React.ChangeEvent<HTMLInputElement>) => 
    {
        setIsUserActivated({ ...isUserActivated, [event.target.name]: event.target.checked });
    };

    const passwordFormHandler = (event: React.ChangeEvent<HTMLInputElement>) => 
    {
        setPasswordForm({ ...passwordForm, [event.currentTarget.name]: event.currentTarget.value }); 
    };

    const accountButtonHandler = () => 
    {
        let validationResult = ValidateAccountForm( 
        { 
            firstName: accountForm.firstName,
            lastName: accountForm.lastName, 
            email: accountForm.email,
            shortBio: accountForm.shortBio
        });

        if (!Validate.isDefined(validationResult))
        {
            setAccountFormProgress(true);
            return;
        }

        showWarning(GetTextWarning({ object: validationResult, template: UPDATE_USER_WARNING }));
    };

    const passwordButtonHandler = () => 
    {
        let validationResult = ValidatePasswordForm( 
        {
            oldPassword: passwordForm.oldPassword,
            newPassword: passwordForm.newPassword,
            confirmPassword: passwordForm.confirmPassword
        });
    
        if (!Validate.isDefined(validationResult))
        {
            setPasswordFormProgress(true);
            return;
        }
    
        showWarning(GetTextWarning({ object: validationResult, template: UPDATE_USER_WARNING }));
    };

    return(
        <UserAccountView bind=
        {{
            isLoading: props.isLoading,
            isAnonymous: isAnonymous,
            userId: userDataState.userId,
            userAlias: userDataState.aliasName,
            firstName: accountForm.firstName,
            lastName: accountForm.lastName,
            email: accountForm.email,
            shortBio: accountForm.shortBio,
            userAvatar: userDataState.avatarName,
            isUserActivated: isUserActivated.checked,
            accountFormProgress: accountFormProgress,           
            accountFormHandler: accountFormHandler,
            accountSwitchHandler: accountSwitchHandler,
            accountButtonHandler: accountButtonHandler,
            avatarUploadProgress: false,
            avatarButtonHandler: null,
            oldPassword: passwordForm.oldPassword,
            newPassword: passwordForm.newPassword,
            confirmPassword: passwordForm.confirmPassword,
            passwordFormProgress: passwordFormProgress,
            passwordFormHandler: passwordFormHandler,
            passwordButtonHandler: passwordButtonHandler,
            sectionAccessDenied: props.content?.sectionAccessDenied,
            sectionAccountInformation: props.content?.sectionAccountInformation,
            sectionAccountPassword: props.content?.sectionAccountPassword,
            sectionAccountRemoval: props.content?.sectionAccountRemoval
        }} />
    );
}

export default UserAccount;
