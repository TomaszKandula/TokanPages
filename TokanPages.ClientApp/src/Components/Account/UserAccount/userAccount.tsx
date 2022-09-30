import * as React from "react";
import { useHistory } from "react-router";
import { useDispatch, useSelector } from "react-redux";
import { IApplicationState } from "../../../Store/Configuration";
import { IGetAccountContent } from "../../../Store/States";

import { 
    ApplicationDialog, 
    UserUpdateAction, 
    UserDataAction, 
    UserSigninAction, 
    UserUpdatePasswordAction, 
    UserRemoveAction, 
    UserReAuthenticateAction 
} from "../../../Store/Actions";

import SuccessMessage from "../../../Shared/Components/ApplicationDialogBox/Helpers/successMessage";
import WarningMessage from "../../../Shared/Components/ApplicationDialogBox/Helpers/warningMessage";
import { GetTextWarning } from "../../../Shared/Services/Utilities";
import { OperationStatus } from "../../../Shared/enums";
import { IRemoveUserDto, IUpdateUserDto, IUpdateUserPasswordDto } from "../../../Api/Models";
import { UserAccountView } from "./View/userAccountView";
import Validate from "validate.js";

import { 
    IValidateAccountForm, 
    IValidatePasswordForm, 
    ValidateAccountForm, 
    ValidatePasswordForm 
} from "../../../Shared/Services/FormValidation";

import { 
    ACCOUNT_FORM, 
    DEACTIVATE_USER, 
    RECEIVED_ERROR_MESSAGE, 
    REMOVE_USER, 
    UPDATE_PASSWORD_SUCCESS, 
    UPDATE_USER_SUCCESS, 
    UPDATE_USER_WARNING 
} from "../../../Shared/constants";

export const UserAccount = (props: IGetAccountContent): JSX.Element => 
{
    const dispatch = useDispatch();
    const history = useHistory();
    
    const userDataState = useSelector((state: IApplicationState) => state.storeUserData.userData);
    const removeAccountState = useSelector((state: IApplicationState) => state.removeAccount);
    const updatePasswordState = useSelector((state: IApplicationState) => state.updateUserPassword);
    const updateUserState = useSelector((state: IApplicationState) => state.updateUser);
    const raiseErrorState = useSelector((state: IApplicationState) => state.raiseError);
    const isAnonymous = Validate.isEmpty(userDataState.userId);

    const accountFormDefault: IValidateAccountForm = 
    {
        firstName: userDataState.firstName,
        lastName: userDataState.lastName,
        email: userDataState.email,
        userAboutText: userDataState.shortBio ?? ""
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
    const [deleteAccountProgress, setDeleteAccountProgress] = React.useState(false);

    const showSuccess = React.useCallback((text: string) => dispatch(ApplicationDialog.raise(SuccessMessage(ACCOUNT_FORM, text))), [ dispatch ]);
    const showWarning = React.useCallback((text: string)=> dispatch(ApplicationDialog.raise(WarningMessage(ACCOUNT_FORM, text))), [ dispatch ]);

    const postUpdateUser = React.useCallback((payload: IUpdateUserDto) => dispatch(UserUpdateAction.update(payload)), [ dispatch ]);
    const postUpdateUserClear = React.useCallback(() => dispatch(UserUpdateAction.clear()), [ dispatch ]);

    const postUpdatePassword = React.useCallback((payload: IUpdateUserPasswordDto) => dispatch(UserUpdatePasswordAction.update(payload)), [ dispatch ]);
    const postUpdatePasswordClear = React.useCallback(() => dispatch(UserUpdatePasswordAction.clear()), [ dispatch ]);

    const postRemoveAccount = React.useCallback((payload: IRemoveUserDto) => dispatch(UserRemoveAction.remove(payload)), [ dispatch ]);
    const postRemoveAccountClear = React.useCallback(() => dispatch(UserRemoveAction.clear), [ dispatch ]);

    const reAuthenticate = React.useCallback(() => dispatch(UserReAuthenticateAction.reAuthenticate()), [ dispatch ]);
    const clearLoggedUser = React.useCallback(() => dispatch(UserSigninAction.clear()), [ dispatch ]);
    const clearLoggedData = React.useCallback(() => dispatch(UserDataAction.clear()), [ dispatch ]);

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

    const removeAccountClear = React.useCallback(() => 
    {
        if (!deleteAccountProgress) return;

        postRemoveAccountClear();
        setDeleteAccountProgress(false);
        clearLoggedUser();
        clearLoggedData();
        history.push("/");
    }, 
    [ deleteAccountProgress, postRemoveAccountClear, clearLoggedUser, clearLoggedData ]);

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
                    userAboutText: accountForm.userAboutText
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

    React.useEffect(() => 
    {
        if (raiseErrorState?.defaultErrorMessage === RECEIVED_ERROR_MESSAGE)
        {
            setDeleteAccountProgress(false);
            return;
        }

        switch(removeAccountState?.operationStatus)
        {
            case OperationStatus.notStarted:
                if (deleteAccountProgress) postRemoveAccount({ });
            break;

            case OperationStatus.hasFinished:
                removeAccountClear();
                showSuccess(REMOVE_USER);
            break;
        }
    }, 
    [ deleteAccountProgress, raiseErrorState?.defaultErrorMessage, removeAccountState?.operationStatus, 
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
            userAboutText: accountForm.userAboutText
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

    const deleteButtonHandler = () => 
    {
        if (!deleteAccountProgress) setDeleteAccountProgress(true);
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
            userAboutText: accountForm.userAboutText,
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
            deleteButtonHandler: deleteButtonHandler,
            deleteAccountProgress: deleteAccountProgress,
            sectionAccessDenied: props.content?.sectionAccessDenied,
            sectionAccountInformation: props.content?.sectionAccountInformation,
            sectionAccountPassword: props.content?.sectionAccountPassword,
            sectionAccountRemoval: props.content?.sectionAccountRemoval
        }} />
    );
}
