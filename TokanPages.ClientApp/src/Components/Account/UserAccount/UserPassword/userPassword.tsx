import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { IApplicationState } from "../../../../Store/Configuration";
import { IContentAccount } from "../../../../Store/States";
import { OperationStatus } from "../../../../Shared/enums";
import { UserPasswordView } from "./View/userPasswordView";
import Validate from "validate.js";

import { 
    ApplicationDialogAction, 
    UserPasswordUpdateAction, 
} from "../../../../Store/Actions";

import { 
    GetTextWarning, 
    SuccessMessage, 
    WarningMessage 
} from "../../../../Shared/Services/Utilities";

import { 
    IValidatePasswordForm, 
    ValidatePasswordForm 
} from "../../../../Shared/Services/FormValidation";

import { 
    ACCOUNT_FORM, 
    RECEIVED_ERROR_MESSAGE, 
    UPDATE_PASSWORD_SUCCESS, 
    UPDATE_USER_WARNING 
} from "../../../../Shared/constants";

export const UserPassword = (props: IContentAccount): JSX.Element => 
{
    const dispatch = useDispatch();
    
    const password = useSelector((state: IApplicationState) => state.userPasswordUpdate);
    const error = useSelector((state: IApplicationState) => state.applicationError);

    const passwordFormDefault: IValidatePasswordForm = 
    {
        oldPassword: "",
        newPassword: "",
        confirmPassword: ""
    }

    const [passwordForm, setPasswordForm] = React.useState(passwordFormDefault);
    const [passwordFormProgress, setPasswordFormProgress] = React.useState(false);

    const showSuccess = (text: string) => dispatch(ApplicationDialogAction.raise(SuccessMessage(ACCOUNT_FORM, text)));
    const showWarning = (text: string)=> dispatch(ApplicationDialogAction.raise(WarningMessage(ACCOUNT_FORM, text)));

    const passwordFormClear = React.useCallback(() => 
    {
        if (!passwordFormProgress) return;

        dispatch(UserPasswordUpdateAction.clear());
        setPasswordForm(passwordFormDefault);
        setPasswordFormProgress(false);
    }, 
    [ passwordFormProgress ]);

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

    const passwordFormHandler = (event: React.ChangeEvent<HTMLInputElement>) => 
    {
        setPasswordForm({ ...passwordForm, [event.currentTarget.name]: event.currentTarget.value }); 
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
        <UserPasswordView bind=
        {{
            isLoading: props.isLoading,
            oldPassword: passwordForm.oldPassword,
            newPassword: passwordForm.newPassword,
            confirmPassword: passwordForm.confirmPassword,
            passwordFormProgress: passwordFormProgress,
            passwordFormHandler: passwordFormHandler,
            passwordButtonHandler: passwordButtonHandler,
            sectionAccessDenied: props.content?.sectionAccessDenied,
            sectionAccountPassword: props.content?.sectionAccountPassword,
        }} />
    );
}
