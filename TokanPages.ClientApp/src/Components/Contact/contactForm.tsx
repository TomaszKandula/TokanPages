import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../Store/Configuration";
import { OperationStatus } from "../../Shared/enums";
import { ReactChangeEvent, ReactKeyboardEvent } from "../../Shared/types";
import { ContactFormView } from "./View/contactFormView";
import Validate from "validate.js";

import { ApplicationDialogAction, ApplicationMessageAction } from "../../Store/Actions";

import { ContactFormInput, ValidateContactForm } from "../../Shared/Services/FormValidation";

import { GetTextWarning, SuccessMessage, WarningMessage } from "../../Shared/Services/Utilities";

import { RECEIVED_ERROR_MESSAGE } from "../../Shared/constants";

const formDefault: ContactFormInput = {
    firstName: "",
    lastName: "",
    email: "",
    subject: "",
    message: "",
    terms: false,
};

export const ContactForm = (): JSX.Element => {
    const dispatch = useDispatch();

    const content = useSelector((state: ApplicationState) => state.contentTemplates?.content);
    const contactForm = useSelector((state: ApplicationState) => state.contentContactForm);
    const email = useSelector((state: ApplicationState) => state.applicationEmail);
    const error = useSelector((state: ApplicationState) => state.applicationError);

    const hasNotStarted = email?.status === OperationStatus.notStarted;
    const hasFinished = email?.status === OperationStatus.hasFinished;
    const hasError = error?.errorMessage === RECEIVED_ERROR_MESSAGE;

    const [form, setForm] = React.useState(formDefault);
    const [hasProgress, setHasProgress] = React.useState(false);

    const showSuccess = (text: string) =>
        dispatch(ApplicationDialogAction.raise(SuccessMessage(content.forms.textContactForm, text)));
    const showWarning = (text: string) =>
        dispatch(ApplicationDialogAction.raise(WarningMessage(content.forms.textContactForm, text)));

    const clearForm = React.useCallback(() => {
        if (!hasProgress) return;
        setHasProgress(false);
        dispatch(ApplicationMessageAction.clear());
    }, [hasProgress]);

    React.useEffect(() => {
        if (hasError) {
            clearForm();
            return;
        }

        if (hasNotStarted && hasProgress) {
            dispatch(
                ApplicationMessageAction.send({
                    firstName: form.firstName,
                    lastName: form.lastName,
                    userEmail: form.email,
                    emailFrom: form.email,
                    emailTos: [form.email],
                    subject: form.subject,
                    message: form.message,
                })
            );

            return;
        }

        if (hasFinished) {
            clearForm();
            setForm(formDefault);
            showSuccess(content.templates.messageOut.success);
        }
    }, [hasProgress, hasError, hasNotStarted, hasFinished, content]);

    const keyHandler = React.useCallback(
        (event: ReactKeyboardEvent) => {
            if (event.code === "Enter") {
                buttonHandler();
            }
        },
        [form.email, form.firstName, form.lastName, form.subject]
    );

    const formHandler = React.useCallback(
        (event: ReactChangeEvent) => {
            if (event.currentTarget.name !== "terms") {
                setForm({ ...form, [event.currentTarget.name]: event.currentTarget.value });
                return;
            }

            setForm({ ...form, [event.currentTarget.name]: event.currentTarget.checked });
        },
        [form]
    );

    const buttonHandler = React.useCallback(() => {
        const result = ValidateContactForm({
            firstName: form.firstName,
            lastName: form.lastName,
            email: form.email,
            subject: form.subject,
            message: form.message,
            terms: form.terms,
        });

        if (!Validate.isDefined(result)) {
            setHasProgress(true);
            return;
        }

        showWarning(GetTextWarning({ object: result, template: content.templates.messageOut.warning }));
    }, [form, content]);

    return (
        <ContactFormView
            isLoading={contactForm?.isLoading}
            caption={contactForm?.content?.caption}
            text={contactForm?.content?.text}
            keyHandler={keyHandler}
            formHandler={formHandler}
            firstName={form.firstName}
            lastName={form.lastName}
            email={form.email}
            subject={form.subject}
            message={form.message}
            terms={form.terms}
            buttonHandler={buttonHandler}
            progress={hasProgress}
            buttonText={contactForm?.content?.button}
            consent={contactForm?.content?.consent}
            labelFirstName={contactForm?.content?.labelFirstName}
            labelLastName={contactForm?.content?.labelLastName}
            labelEmail={contactForm?.content?.labelEmail}
            labelSubject={contactForm?.content?.labelSubject}
            labelMessage={contactForm?.content?.labelMessage}
            multiline={true}
            minRows={6}
        />
    );
};
