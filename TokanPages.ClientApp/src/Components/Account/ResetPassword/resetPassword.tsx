import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { IApplicationState } from "../../../Store/Configuration";
import { IContentResetPassword } from "../../../Store/States";
import { ApplicationDialogAction, UserPasswordResetAction } from "../../../Store/Actions";
import { IValidateResetForm, ValidateResetForm } from "../../../Shared/Services/FormValidation";
import { GetTextWarning } from "../../../Shared/Services/Utilities";
import { OperationStatus } from "../../../Shared/enums";
import { ResetPasswordView } from "./View/resetPasswordView";
import Validate from "validate.js";

import { 
    RECEIVED_ERROR_MESSAGE, 
    RESET_FORM, 
    RESET_PASSWORD_SUCCESS, 
    RESET_PASSWORD_WARNING 
} from "../../../Shared/constants";

const formDefaultValues: IValidateResetForm =
{
    email: ""
};

export const ResetPassword = (props: IContentResetPassword): JSX.Element =>
{
    const dispatch = useDispatch();
    const appState = useSelector((state: IApplicationState) => state.userPasswordReset);
    const appError = useSelector((state: IApplicationState) => state.applicationError);

    const raiseSuccess = (text: string) => ApplicationDialogAction.raise(SuccessMessage(RESET_FORM, text));
    const raiseWarning = (text: string) => ApplicationDialogAction.raise(WarningMessage(RESET_FORM, text));

    const showSuccess = React.useCallback((text: string) => dispatch(raiseSuccess(text)), [ dispatch ]);
    const showWarning = React.useCallback((text: string) => dispatch(raiseWarning(text)), [ dispatch ]);

    const [form, setForm] = React.useState(formDefaultValues);
    const [progress, setProgress] = React.useState(false);

    const clearForm = React.useCallback(() => 
    {
        if (!progress) return;
        setProgress(false);
        dispatch(UserPasswordResetAction.clear());
    }, 
    [ progress ]);

    React.useEffect(() => 
    {
        if (appError?.defaultErrorMessage === RECEIVED_ERROR_MESSAGE)
        {
            clearForm();
            return;
        }

        switch(appState?.operationStatus)
        {
            case OperationStatus.notStarted:
                if (progress) dispatch(UserPasswordResetAction.reset(
                {
                    emailAddress: form.email
                }));
            break;

            case OperationStatus.hasFinished:
                clearForm();
                setForm(formDefaultValues);
                showSuccess(RESET_PASSWORD_SUCCESS);
            break;
        }
    }, 
    [ progress, appError?.defaultErrorMessage, appState?.operationStatus, 
    OperationStatus.notStarted, OperationStatus.hasFinished ]);

    const formHandler = (event: React.ChangeEvent<HTMLInputElement>) => 
    { 
        setForm({ ...form, [event.currentTarget.name]: event.currentTarget.value }); 
    };

    const buttonHandler = () =>
    {
        let results = ValidateResetForm({ email: form.email });

        if (!Validate.isDefined(results))
        {
            setProgress(true);
            return;
        }

        showWarning(GetTextWarning({ object: results, template: RESET_PASSWORD_WARNING }));
    };

    return (
        <ResetPasswordView bind=
        {{
            isLoading: props.isLoading,
            progress: progress,
            caption: props.content.caption,
            button: props.content.button,
            email: form.email,
            formHandler: formHandler,
            buttonHandler: buttonHandler,
            labelEmail: props.content.labelEmail
        }}/>
    );
}
