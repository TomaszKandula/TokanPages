import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../Store/Configuration";
import { ApplicationDialogAction, ApplicationMessageAction } from "../../Store/Actions";
import { useDimensions } from "../../Shared/Hooks";
import { ContactFormInput, ValidateContactForm } from "../../Shared/Services/FormValidation";
import { RECEIVED_ERROR_MESSAGE } from "../../Shared/constants";
import { IconType, OperationStatus } from "../../Shared/enums";
import { ReactChangeEvent, ReactChangeTextEvent, ReactKeyboardEvent } from "../../Shared/types";
import { ContactFormView } from "./View/contactFormView";
import Validate from "validate.js";

const formDefault: ContactFormInput = {
    firstName: "",
    lastName: "",
    email: "",
    subject: "",
    message: "",
    terms: true,
};

export interface ContactFormProps {
    hasCaption?: boolean;
    hasIcon?: boolean;
    hasShadow?: boolean;
    className?: string;
}

export const ContactForm = (props: ContactFormProps): React.ReactElement => {
    const media = useDimensions();
    const dispatch = useDispatch();

    const email = useSelector((state: ApplicationState) => state.applicationEmail);
    const error = useSelector((state: ApplicationState) => state.applicationError);
    const data = useSelector((state: ApplicationState) => state.contentPageData);
    const templates = data?.components?.templates;
    const contactForm = data?.components?.sectionContactForm;

    const hasNotStarted = email?.status === OperationStatus.notStarted;
    const hasFinished = email?.status === OperationStatus.hasFinished;
    const hasError = error?.errorMessage === RECEIVED_ERROR_MESSAGE;

    const [form, setForm] = React.useState(formDefault);
    const [message, setMessage] = React.useState({ message: "" });
    const [hasProgress, setHasProgress] = React.useState(false);

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
                    message: message.message,
                })
            );

            return;
        }

        if (hasFinished) {
            clearForm();
            setForm(formDefault);
            setMessage({ message: "" });
            dispatch(
                ApplicationDialogAction.raise({
                    title: templates.forms.textContactForm,
                    message: templates.templates.messageOut.success,
                    icon: IconType.info,
                })
            );
        }
    }, [hasProgress, hasError, hasNotStarted, hasFinished, templates, message, form]);

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
            setForm({ ...form, [event.currentTarget.name]: event.currentTarget.value });
        },
        [form]
    );

    const messageHandler = React.useCallback(
        (event: ReactChangeTextEvent) => {
            setMessage({ ...message, [event.currentTarget.name]: event.currentTarget.value });
        },
        [message]
    );

    const buttonHandler = React.useCallback(() => {
        const result = ValidateContactForm({
            firstName: form.firstName,
            lastName: form.lastName,
            email: form.email,
            subject: form.subject,
            message: message.message,
            terms: true,
        });

        if (!Validate.isDefined(result)) {
            setHasProgress(true);
            return;
        }

        dispatch(
            ApplicationDialogAction.raise({
                title: templates.forms.textContactForm,
                message: templates.templates.messageOut.warning,
                validation: result,
                icon: IconType.warning,
            })
        );
    }, [form, message, templates]);

    return (
        <ContactFormView
            isLoading={data?.isLoading}
            isMobile={media.isMobile}
            caption={contactForm?.caption}
            text={contactForm?.text}
            keyHandler={keyHandler}
            formHandler={formHandler}
            messageHandler={messageHandler}
            firstName={form.firstName}
            lastName={form.lastName}
            email={form.email}
            subject={form.subject}
            message={message.message}
            buttonHandler={buttonHandler}
            progress={hasProgress}
            buttonText={contactForm?.button}
            consent={contactForm?.consent}
            labelFirstName={contactForm?.labelFirstName}
            labelLastName={contactForm?.labelLastName}
            labelEmail={contactForm?.labelEmail}
            labelSubject={contactForm?.labelSubject}
            labelMessage={contactForm?.labelMessage}
            minRows={6}
            hasIcon={props.hasIcon}
            hasCaption={props.hasCaption}
            hasShadow={props.hasShadow}
            className={props.className}
        />
    );
};
