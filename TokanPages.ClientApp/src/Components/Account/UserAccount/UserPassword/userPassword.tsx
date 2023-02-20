import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../../../Store/Configuration";
import { ContentAccountState } from "../../../../Store/States";
import { OperationStatus } from "../../../../Shared/enums";
import { ReactChangeEvent, ReactKeyboardEvent } from "../../../../Shared/types";
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
    PasswordFormInput, 
    ValidatePasswordForm 
} from "../../../../Shared/Services/FormValidation";

import { 
    ACCOUNT_FORM, 
    RECEIVED_ERROR_MESSAGE, 
    UPDATE_PASSWORD_SUCCESS, 
    UPDATE_USER_WARNING 
} from "../../../../Shared/constants";

export const UserPassword = (props: ContentAccountState): JSX.Element => 
{
    const dispatch = useDispatch();
    
    const update = useSelector((state: ApplicationState) => state.userPasswordUpdate);
    const error = useSelector((state: ApplicationState) => state.applicationError);

    const hasNotStarted = update?.status === OperationStatus.notStarted;
    const hasFinished = update?.status === OperationStatus.hasFinished;
    const hasError = error?.errorMessage === RECEIVED_ERROR_MESSAGE;

    const passwordFormDefault: PasswordFormInput = 
    {
        oldPassword: "",
        newPassword: "",
        confirmPassword: ""
    }

    const [passwordForm, setPasswordForm] = React.useState(passwordFormDefault);
    const [hasProgress, setHasProgress] = React.useState(false);

    const showSuccess = (text: string) => dispatch(ApplicationDialogAction.raise(SuccessMessage(ACCOUNT_FORM, text)));
    const showWarning = (text: string)=> dispatch(ApplicationDialogAction.raise(WarningMessage(ACCOUNT_FORM, text)));

    const clear = React.useCallback(() => 
    {
        if (!hasProgress) return;

        dispatch(UserPasswordUpdateAction.clear());
        setPasswordForm(passwordFormDefault);
        setHasProgress(false);
    }, 
    [ hasProgress ]);

    React.useEffect(() => 
    {
        if (hasError)
        {
            clear();
            return;
        }

        if (hasNotStarted && hasProgress)
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
    [ hasProgress, hasError, hasNotStarted, hasFinished ]);

    const passwordKeyHandler = React.useCallback((event: ReactKeyboardEvent) => 
    {
        if (event.code === "Enter")
        {
            passwordButtonHandler();
        }
    }, []);

    const passwordFormHandler = React.useCallback((event: ReactChangeEvent) => 
    {
        setPasswordForm({ ...passwordForm, [event.currentTarget.name]: event.currentTarget.value }); 
    }, [ passwordForm ]);

    const passwordButtonHandler = React.useCallback(() => 
    {
        let validationResult = ValidatePasswordForm(passwordForm);
        if (!Validate.isDefined(validationResult))
        {
            setHasProgress(true);
            return;
        }
    
        showWarning(GetTextWarning({ object: validationResult, template: UPDATE_USER_WARNING }));
    }, [ passwordForm ]);

    return(
        <UserPasswordView
            isLoading={props.isLoading}
            oldPassword={passwordForm.oldPassword}
            newPassword={passwordForm.newPassword}
            confirmPassword={passwordForm.confirmPassword}
            passwordKeyHandler={passwordKeyHandler}
            passwordFormProgress={hasProgress}
            passwordFormHandler={passwordFormHandler}
            passwordButtonHandler={passwordButtonHandler}
            sectionAccessDenied={props.content?.sectionAccessDenied}
            sectionAccountPassword={props.content?.sectionAccountPassword}
        />
    );
}
