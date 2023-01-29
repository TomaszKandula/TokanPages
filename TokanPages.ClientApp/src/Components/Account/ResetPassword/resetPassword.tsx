import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../../Store/Configuration";
import { ContentResetPasswordState } from "../../../Store/States";
import { OperationStatus } from "../../../Shared/enums";
import { ResetPasswordView } from "./View/resetPasswordView";
import Validate from "validate.js";

import { 
    ApplicationDialogAction, 
    UserPasswordResetAction 
} from "../../../Store/Actions";

import { 
    ResetFormInput, 
    ValidateResetForm 
} from "../../../Shared/Services/FormValidation";

import { 
    GetTextWarning, 
    SuccessMessage, 
    WarningMessage 
} from "../../../Shared/Services/Utilities";

import { 
    RECEIVED_ERROR_MESSAGE, 
    RESET_FORM, 
    RESET_PASSWORD_SUCCESS, 
    RESET_PASSWORD_WARNING 
} from "../../../Shared/constants";

const formDefaultValues: ResetFormInput =
{
    email: ""
};

export const ResetPassword = (props: ContentResetPasswordState): JSX.Element =>
{
    const dispatch = useDispatch();
    const reset = useSelector((state: ApplicationState) => state.userPasswordReset);
    const error = useSelector((state: ApplicationState) => state.applicationError);

    const hasNotStarted = reset?.status === OperationStatus.notStarted;
    const hasFinished = reset?.status === OperationStatus.hasFinished;
    const hasError = error?.errorMessage === RECEIVED_ERROR_MESSAGE;

    const showSuccess = (text: string) => dispatch(ApplicationDialogAction.raise(SuccessMessage(RESET_FORM, text)));
    const showWarning = (text: string) => dispatch(ApplicationDialogAction.raise(WarningMessage(RESET_FORM, text)));

    const [form, setForm] = React.useState(formDefaultValues);
    const [hasProgress, setHasProgress] = React.useState(false);

    const clearForm = React.useCallback(() => 
    {
        if (!hasProgress) return;
        setHasProgress(false);
        dispatch(UserPasswordResetAction.clear());
    }, 
    [ hasProgress ]);

    React.useEffect(() => 
    {
        if (hasError)
        {
            clearForm();
            return;
        }

        if (hasNotStarted && hasProgress) 
        {
            dispatch(UserPasswordResetAction.reset(
            {
                emailAddress: form.email
            }));

            return;
        }

        if (hasFinished)
        {
            clearForm();
            setForm(formDefaultValues);
            showSuccess(RESET_PASSWORD_SUCCESS);
        }
    }, 
    [ hasProgress, hasError, hasNotStarted, hasFinished ]);

    const keyHandler = (event: React.KeyboardEvent<HTMLInputElement>) => 
    {
        if (event.code === "Enter")
        {
            buttonHandler();
        }
    }

    const formHandler = (event: React.ChangeEvent<HTMLInputElement>) => 
    { 
        setForm({ ...form, [event.currentTarget.name]: event.currentTarget.value }); 
    };

    const buttonHandler = () =>
    {
        let results = ValidateResetForm({ email: form.email });

        if (!Validate.isDefined(results))
        {
            setHasProgress(true);
            return;
        }

        showWarning(GetTextWarning({ object: results, template: RESET_PASSWORD_WARNING }));
    };

    return (
        <ResetPasswordView
            isLoading={props.isLoading}
            progress={hasProgress}
            caption={props.content.caption}
            button={props.content.button}
            email={form.email}
            keyHandler={keyHandler}
            formHandler={formHandler}
            buttonHandler={buttonHandler}
            labelEmail={props.content.labelEmail}
        />
    );
}
