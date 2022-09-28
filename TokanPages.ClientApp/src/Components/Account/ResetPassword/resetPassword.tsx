import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { IApplicationState } from "../../../Store/Configuration";
import { IGetResetPasswordContent } from "../../../Store/States";
import { DialogAction } from "../../../Store/Actions";
import { UserResetPasswordAction } from "../../../Store/Actions";
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

export const ResetPassword = (props: IGetResetPasswordContent): JSX.Element =>
{
    const dispatch = useDispatch();
    const resetUserPasswordState = useSelector((state: IApplicationState) => state.resetUserPassword);
    const raiseErrorState = useSelector((state: IApplicationState) => state.raiseError);
    
    const showSuccess = React.useCallback((text: string) => dispatch(DialogAction.raiseDialog(SuccessMessage(RESET_FORM, text))), [ dispatch ]);
    const showWarning = React.useCallback((text: string) => dispatch(DialogAction.raiseDialog(WarningMessage(RESET_FORM, text))), [ dispatch ]);

    const [form, setForm] = React.useState(formDefaultValues);
    const [progress, setProgress] = React.useState(false);

    const resetAction = React.useCallback((payload: IResetUserPasswordDto) => dispatch(UserResetPasswordAction.reset(payload)), [ dispatch ]);
    const clearAction = React.useCallback(() => dispatch(UserResetPasswordAction.clear()), [ dispatch ]);
    const clearForm = React.useCallback(() => 
    {
        if (!progress) return;
        setProgress(false);
        clearAction();
    }, 
    [ progress, clearAction ]);

    React.useEffect(() => 
    {
        if (raiseErrorState?.defaultErrorMessage === RECEIVED_ERROR_MESSAGE)
        {
            clearForm();
            return;
        }

        switch(resetUserPasswordState?.operationStatus)
        {
            case OperationStatus.notStarted:
                if (progress) resetAction(
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
    [ progress, raiseErrorState?.defaultErrorMessage, resetUserPasswordState?.operationStatus, 
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
