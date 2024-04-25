import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ReactChangeEvent, ReactKeyboardEvent } from "../../Shared/types";
import { ApplicationState } from "../../Store/Configuration";
import { OperationStatus } from "../../Shared/enums";
import { ValidateEmailForm } from "../../Shared/Services/FormValidation";
import { NewsletterView } from "./View/newsletterView";
import Validate from "validate.js";

import { NewsletterAddAction, ApplicationDialogAction } from "../../Store/Actions";

import { GetTextWarning, SuccessMessage, WarningMessage } from "../../Shared/Services/Utilities";

import { NEWSLETTER, NEWSLETTER_SUCCESS, NEWSLETTER_WARNING, RECEIVED_ERROR_MESSAGE } from "../../Shared/constants";

export const Newsletter = (): JSX.Element => {
    const dispatch = useDispatch();

    const newsletter = useSelector((state: ApplicationState) => state.contentNewsletter);
    const add = useSelector((state: ApplicationState) => state.newsletterAdd);
    const error = useSelector((state: ApplicationState) => state.applicationError);

    const hasNotStarted = add?.status === OperationStatus.notStarted;
    const hasFinished = add?.status === OperationStatus.hasFinished;
    const hasError = error?.errorMessage === RECEIVED_ERROR_MESSAGE;

    const [form, setForm] = React.useState({ email: "" });
    const [hasProgress, setHasProgress] = React.useState(false);

    const showSuccess = (text: string) => dispatch(ApplicationDialogAction.raise(SuccessMessage(NEWSLETTER, text)));
    const showWarning = (text: string) => dispatch(ApplicationDialogAction.raise(WarningMessage(NEWSLETTER, text)));

    const clearForm = React.useCallback(() => {
        if (!hasProgress) return;
        setHasProgress(false);
        dispatch(NewsletterAddAction.clear());
    }, [hasProgress]);

    React.useEffect(() => {
        if (hasError) {
            clearForm();
            return;
        }

        if (hasNotStarted && hasProgress) {
            dispatch(NewsletterAddAction.add({ email: form.email }));
            return;
        }

        if (hasFinished) {
            clearForm();
            setForm({ email: "" });
            showSuccess(NEWSLETTER_SUCCESS);
        }
    }, [hasProgress, hasError, hasNotStarted, hasFinished]);

    const keyHandler = React.useCallback(
        (event: ReactKeyboardEvent) => {
            if (event.code === "Enter") {
                buttonHandler();
            }
        },
        [form.email]
    );

    const formHandler = React.useCallback(
        (event: ReactChangeEvent) => {
            setForm({ ...form, [event.currentTarget.name]: event.currentTarget.value });
        },
        [form]
    );

    const buttonHandler = React.useCallback(() => {
        const result = ValidateEmailForm({ email: form.email });

        if (!Validate.isDefined(result)) {
            setHasProgress(true);
            return;
        }

        showWarning(GetTextWarning({ object: result, template: NEWSLETTER_WARNING }));
    }, [form]);

    return (
        <NewsletterView
            isLoading={newsletter.isLoading}
            caption={newsletter.content?.caption}
            text={newsletter.content?.text}
            keyHandler={keyHandler}
            formHandler={formHandler}
            email={form.email}
            buttonHandler={buttonHandler}
            progress={hasProgress}
            buttonText={newsletter.content?.button}
            labelEmail={newsletter.content?.labelEmail}
        />
    );
};
