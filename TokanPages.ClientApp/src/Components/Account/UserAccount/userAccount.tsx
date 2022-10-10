import * as React from "react";
import { useHistory } from "react-router";
import { useDispatch, useSelector } from "react-redux";
import { IApplicationState } from "../../../Store/Configuration";
import { IContentAccount } from "../../../Store/States";

import { 
    ApplicationDialogAction, 
    UserUpdateAction, 
    UserDataStoreAction, 
    UserSigninAction, 
    UserPasswordUpdateAction, 
    UserRemoveAction, 
    UserReAuthenticateAction 
} from "../../../Store/Actions";

import { GetTextWarning, SuccessMessage, WarningMessage } from "../../../Shared/Services/Utilities";
import { OperationStatus } from "../../../Shared/enums";
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

export const UserAccount = (props: IContentAccount): JSX.Element => 
{
    const dispatch = useDispatch();
    const history = useHistory();
    
    const data = useSelector((state: IApplicationState) => state.userDataStore.userData);
    const remove = useSelector((state: IApplicationState) => state.userRemove);
    const password = useSelector((state: IApplicationState) => state.userPasswordUpdate);
    const user = useSelector((state: IApplicationState) => state.userUpdate);
    const error = useSelector((state: IApplicationState) => state.applicationError);
    const isAnonymous = Validate.isEmpty(data.userId);

    const accountFormDefault: IValidateAccountForm = 
    {
        firstName: data.firstName,
        lastName: data.lastName,
        email: data.email,
        userAboutText: data.shortBio ?? ""
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

    const showSuccess = (text: string) => dispatch(ApplicationDialogAction.raise(SuccessMessage(ACCOUNT_FORM, text)));
    const showWarning = (text: string)=> dispatch(ApplicationDialogAction.raise(WarningMessage(ACCOUNT_FORM, text)));

    const accountFormClear = React.useCallback(() => 
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

    const passwordFormClear = React.useCallback(() => 
    {
        if (!passwordFormProgress) return;

        dispatch(UserPasswordUpdateAction.clear());
        setPasswordForm(passwordFormDefault);
        setPasswordFormProgress(false);
    }, 
    [ passwordFormProgress ]);

    const removeAccountClear = React.useCallback(() => 
    {
        if (!deleteAccountProgress) return;

        dispatch(UserRemoveAction.clear);
        setDeleteAccountProgress(false);
        dispatch(UserSigninAction.clear());
        dispatch(UserDataStoreAction.clear());
        history.push("/");
    }, 
    [ deleteAccountProgress ]);

    React.useEffect(() => 
    {
        if (error?.defaultErrorMessage === RECEIVED_ERROR_MESSAGE)
        {
            accountFormClear();
            return;
        }

        switch(user?.operationStatus)
        {
            case OperationStatus.notStarted:
                if (accountFormProgress) dispatch(UserUpdateAction.update(
                {
                    id: data.userId,
                    isActivated: isUserActivated.checked,
                    firstName: accountForm.firstName,
                    lastName: accountForm.lastName,
                    emailAddress: accountForm.email,
                    userAboutText: accountForm.userAboutText
                }));
            break;

            case OperationStatus.hasFinished:
                accountFormClear();
                showSuccess(isUserActivated.checked ? UPDATE_USER_SUCCESS : DEACTIVATE_USER);
            break;
        }
    }, 
    [ accountFormProgress, error?.defaultErrorMessage, user?.operationStatus, 
    OperationStatus.notStarted, OperationStatus.hasFinished ]);

    React.useEffect(() => 
    {
        if (error?.defaultErrorMessage === RECEIVED_ERROR_MESSAGE)
        {
            passwordFormClear();
            return;
        }

        switch(password?.operationStatus)
        {
            case OperationStatus.notStarted:
                if (passwordFormProgress) dispatch(UserPasswordUpdateAction.update(
                {
                    oldPassword: passwordForm.oldPassword,
                    newPassword: passwordForm.newPassword
                }));
            break;

            case OperationStatus.hasFinished:
                passwordFormClear();
                showSuccess(UPDATE_PASSWORD_SUCCESS);
            break;
        }
    }, 
    [ passwordFormProgress, error?.defaultErrorMessage, password?.operationStatus, 
    OperationStatus.notStarted, OperationStatus.hasFinished ]);

    React.useEffect(() => 
    {
        if (error?.defaultErrorMessage === RECEIVED_ERROR_MESSAGE)
        {
            setDeleteAccountProgress(false);
            return;
        }

        switch(remove?.operationStatus)
        {
            case OperationStatus.notStarted:
                if (deleteAccountProgress) 
                {
                    dispatch(UserRemoveAction.remove({ }));
                }
            break;

            case OperationStatus.hasFinished:
                removeAccountClear();
                showSuccess(REMOVE_USER);
            break;
        }
    }, 
    [ deleteAccountProgress, error?.defaultErrorMessage, remove?.operationStatus, 
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
        if (!deleteAccountProgress) 
        {
            setDeleteAccountProgress(true);
        }
    };

    return(
        <UserAccountView bind=
        {{
            isLoading: props.isLoading,
            isAnonymous: isAnonymous,
            userId: data.userId,
            userAlias: data.aliasName,
            firstName: accountForm.firstName,
            lastName: accountForm.lastName,
            email: accountForm.email,
            userAboutText: accountForm.userAboutText,
            userAvatar: data.avatarName,
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
