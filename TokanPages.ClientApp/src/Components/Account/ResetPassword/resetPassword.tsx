import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { IApplicationState } from "../../../Store/Configuration";
import { IContentResetPassword } from "../../../Store/States";
import { ApplicationDialog, UserResetPasswordAction } from "../../../Store/Actions";
import { IResetUserPasswordDto } from "../../../Api/Models";
import SuccessMessage from "../../../Shared/Components/ApplicationDialogBox/Helpers/successMessage";
import WarningMessage from "../../../Shared/Components/ApplicationDialogBox/Helpers/warningMessage";
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
    const state = useSelector((state: IApplicationState) => state.userPasswordReset);
    const error = useSelector((state: IApplicationState) => state.applicationError);
    
    const showSuccess = React.useCallback((text: string) => dispatch(ApplicationDialog.raise(SuccessMessage(RESET_FORM, text))), [ dispatch ]);
    const showWarning = React.useCallback((text: string) => dispatch(ApplicationDialog.raise(WarningMessage(RESET_FORM, text))), [ dispatch ]);

    const [form, setForm] = React.useState(formDefaultValues);
    const [progress, setProgress] = React.useState(false);

    const reset = React.useCallback((payload: IResetUserPasswordDto) => dispatch(UserResetPasswordAction.reset(payload)), [ dispatch ]);
    const clear = React.useCallback(() => dispatch(UserResetPasswordAction.clear()), [ dispatch ]);

    const clearForm = React.useCallback(() => 
    {
        if (!progress) return;
        setProgress(false);
        clear();
    }, 
    [ progress, clear ]);

    React.useEffect(() => 
    {
        if (error?.defaultErrorMessage === RECEIVED_ERROR_MESSAGE)
        {
            clearForm();
            return;
        }

        switch(state?.operationStatus)
        {
            case OperationStatus.notStarted:
                if (progress) reset(
                {
                    emailAddress: form.email
                });
            break;

            case OperationStatus.hasFinished:
                clearForm();
                setForm(formDefaultValues);
                showSuccess(RESET_PASSWORD_SUCCESS);
            break;
        }
    }, 
    [ progress, error?.defaultErrorMessage, state?.operationStatus, 
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
