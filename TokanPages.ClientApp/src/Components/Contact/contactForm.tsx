import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../Store/Configuration";
import { ApplicationDialogAction, ApplicationMessageAction } from "../../Store/Actions";
import { ContactFormInput, ValidateContactForm } from "../../Shared/Services/FormValidation";
import { GetTextWarning, SuccessMessage, WarningMessage } from "../../Shared/Services/Utilities";
import { RECEIVED_ERROR_MESSAGE } from "../../Shared/constants";
import { OperationStatus } from "../../Shared/enums";
import { ReactChangeEvent, ReactKeyboardEvent } from "../../Shared/types";
import { ContactFormView } from "./View/contactFormView";
import Validate from "validate.js";

const formDefault: ContactFormInput = {
    firstName: "",
    lastName: "",
    email: "",
    subject: "",
    message: "",
    terms: false,
};

export interface ContactFormProps {
    hasCaption?: boolean;
    hasIcon?: boolean;
    hasShadow?: boolean;
    background?: React.CSSProperties;
    className?: string;
}

export const ContactForm = (props: ContactFormProps): React.ReactElement => {
    const dispatch = useDispatch();

    const email = useSelector((state: ApplicationState) => state.applicationEmail);
    const error = useSelector((state: ApplicationState) => state.applicationError);
    const data = useSelector((state: ApplicationState) => state.contentPageData);
    const templates = data?.components?.templates;
    const contactForm = data?.components?.contactForm;

    const hasNotStarted = email?.status === OperationStatus.notStarted;
    const hasFinished = email?.status === OperationStatus.hasFinished;
    const hasError = error?.errorMessage === RECEIVED_ERROR_MESSAGE;

    const [form, setForm] = React.useState(formDefault);
    const [hasProgress, setHasProgress] = React.useState(false);

    const showSuccess = (text: string) =>
        dispatch(ApplicationDialogAction.raise(SuccessMessage(templates.forms.textContactForm, text)));
    const showWarning = (text: string) =>
        dispatch(ApplicationDialogAction.raise(WarningMessage(templates.forms.textContactForm, text)));

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
            showSuccess(templates.templates.messageOut.success);
        }
    }, [hasProgress, hasError, hasNotStarted, hasFinished, templates]);

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

        showWarning(GetTextWarning({ object: result, template: templates.templates.messageOut.warning }));
    }, [form, templates]);

    return (
        <ContactFormView
            isLoading={data?.isLoading}
            caption={contactForm?.caption}
            text={contactForm?.text}
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
            buttonText={contactForm?.button}
            consent={contactForm?.consent}
            labelFirstName={contactForm?.labelFirstName}
            labelLastName={contactForm?.labelLastName}
            labelEmail={contactForm?.labelEmail}
            labelSubject={contactForm?.labelSubject}
            labelMessage={contactForm?.labelMessage}
            multiline={true}
            minRows={6}
            background={props.background}
            hasIcon={props.hasIcon}
            hasCaption={props.hasCaption}
            hasShadow={props.hasShadow}
            className={props.className}
        />
    );
};
