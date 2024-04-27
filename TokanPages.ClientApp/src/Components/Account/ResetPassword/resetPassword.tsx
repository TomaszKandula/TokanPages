import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../../Store/Configuration";
import { OperationStatus } from "../../../Shared/enums";
import { ReactChangeEvent, ReactKeyboardEvent } from "../../../Shared/types";
import { ResetPasswordView } from "./View/resetPasswordView";
import Validate from "validate.js";

import { ApplicationDialogAction, UserPasswordResetAction } from "../../../Store/Actions";

import { ResetFormInput, ValidateResetForm } from "../../../Shared/Services/FormValidation";

import { GetTextWarning, SuccessMessage, WarningMessage } from "../../../Shared/Services/Utilities";

import { RECEIVED_ERROR_MESSAGE } from "../../../Shared/constants";

const formDefaultValues: ResetFormInput = {
    email: "",
};

export const ResetPassword = (): JSX.Element => {
    const dispatch = useDispatch();

    const template = useSelector((state: ApplicationState) => state.contentTemplates?.content); 
    const content = useSelector((state: ApplicationState) => state.contentResetPassword);
    const reset = useSelector((state: ApplicationState) => state.userPasswordReset);
    const error = useSelector((state: ApplicationState) => state.applicationError);

    const hasNotStarted = reset?.status === OperationStatus.notStarted;
    const hasFinished = reset?.status === OperationStatus.hasFinished;
    const hasError = error?.errorMessage === RECEIVED_ERROR_MESSAGE;

    const showSuccess = (text: string) => dispatch(ApplicationDialogAction.raise(SuccessMessage(template.forms.textPasswordReset, text)));
    const showWarning = (text: string) => dispatch(ApplicationDialogAction.raise(WarningMessage(template.forms.textPasswordReset, text)));

    const [form, setForm] = React.useState(formDefaultValues);
    const [hasProgress, setHasProgress] = React.useState(false);

    const clearForm = React.useCallback(() => {
        if (!hasProgress) return;
        setHasProgress(false);
        dispatch(UserPasswordResetAction.clear());
    }, [hasProgress]);

    React.useEffect(() => {
        if (hasError) {
            clearForm();
            return;
        }

        if (hasNotStarted && hasProgress) {
            dispatch(
                UserPasswordResetAction.reset({
                    emailAddress: form.email,
                })
            );

            return;
        }

        if (hasFinished) {
            clearForm();
            setForm(formDefaultValues);
            showSuccess(template.templates.password.resetSuccess);
        }
    }, [hasProgress, hasError, hasNotStarted, hasFinished, template]);

    const keyHandler = React.useCallback((event: ReactKeyboardEvent) => {
        if (event.code === "Enter") {
            buttonHandler();
        }
    }, []);

    const formHandler = React.useCallback(
        (event: ReactChangeEvent) => {
            setForm({ ...form, [event.currentTarget.name]: event.currentTarget.value });
        },
        [form]
    );

    const buttonHandler = React.useCallback(() => {
        let results = ValidateResetForm({ email: form.email });

        if (!Validate.isDefined(results)) {
            setHasProgress(true);
            return;
        }

        showWarning(GetTextWarning({ object: results, template: template.templates.password.resetWarning }));
    }, [form, template]);

    return (
        <ResetPasswordView
            isLoading={content?.isLoading}
            progress={hasProgress}
            caption={content?.content?.caption}
            button={content?.content?.button}
            email={form.email}
            keyHandler={keyHandler}
            formHandler={formHandler}
            buttonHandler={buttonHandler}
            labelEmail={content?.content?.labelEmail}
        />
    );
};
