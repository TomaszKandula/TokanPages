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
    
    const update = useSelector((state: IApplicationState) => state.userPasswordUpdate);
    const error = useSelector((state: IApplicationState) => state.applicationError);

    const hasNotStarted = update?.status === OperationStatus.notStarted;
    const hasFinished = update?.status === OperationStatus.hasFinished;
    const hasError = error?.errorMessage === RECEIVED_ERROR_MESSAGE;

    const passwordFormDefault: IValidatePasswordForm = 
    {
        oldPassword: "",
        newPassword: "",
        confirmPassword: ""
    }

    const [passwordForm, setPasswordForm] = React.useState(passwordFormDefault);
    const [progress, setProgress] = React.useState(false);

    const showSuccess = (text: string) => dispatch(ApplicationDialogAction.raise(SuccessMessage(ACCOUNT_FORM, text)));
    const showWarning = (text: string)=> dispatch(ApplicationDialogAction.raise(WarningMessage(ACCOUNT_FORM, text)));

    const clear = React.useCallback(() => 
    {
        if (!progress) return;

        dispatch(UserPasswordUpdateAction.clear());
        setPasswordForm(passwordFormDefault);
        setProgress(false);
    }, 
    [ progress ]);

    React.useEffect(() => 
    {
        if (hasError)
        {
            clear();
            return;
        }

        if (hasNotStarted && progress)
        {
            dispatch(UserPasswordUpdateAction.update(passwordForm));
            return;
        }

        if (hasFinished)
        {
            clear();
            showSuccess(UPDATE_PASSWORD_SUCCESS);
        }
    }, 
    [ progress, hasError, hasNotStarted, hasFinished ]);

    const passwordKeyHandler = (event: React.KeyboardEvent<HTMLInputElement>) => 
    {
        if (event.code === "Enter")
        {
            passwordButtonHandler();
        }
    }

    const passwordFormHandler = (event: React.ChangeEvent<HTMLInputElement>) => 
    {
        setPasswordForm({ ...passwordForm, [event.currentTarget.name]: event.currentTarget.value }); 
    };

    const passwordButtonHandler = () => 
    {
        let validationResult = ValidatePasswordForm(passwordForm);
        if (!Validate.isDefined(validationResult))
        {
            setProgress(true);
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
            passwordKeyHandler: passwordKeyHandler,
            passwordFormProgress: progress,
            passwordFormHandler: passwordFormHandler,
            passwordButtonHandler: passwordButtonHandler,
            sectionAccessDenied: props.content?.sectionAccessDenied,
            sectionAccountPassword: props.content?.sectionAccountPassword,
        }} />
    );
}
