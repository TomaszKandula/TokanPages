import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../Store/Configuration";
import { ContentContactFormState } from "../../Store/States";
import { OperationStatus } from "../../Shared/enums";
import { ReactChangeEvent, ReactKeyboardEvent } from "../../Shared/types";
import { ContactFormView } from "./View/contactFormView";
import Validate from "validate.js";

import { 
    ApplicationDialogAction, 
    ApplicationMessageAction 
} from "../../Store/Actions";

import { 
    ContactFormInput, 
    ValidateContactForm 
} from "../../Shared/Services/FormValidation";

import { 
    GetTextWarning, 
    SuccessMessage, 
    WarningMessage 
} from "../../Shared/Services/Utilities";

import { 
    CONTACT_FORM, 
    MESSAGE_OUT_SUCCESS, 
    MESSAGE_OUT_WARNING, 
    RECEIVED_ERROR_MESSAGE 
} from "../../Shared/constants";

const formDefault: ContactFormInput =
{
    firstName: "", 
    lastName: "", 
    email: "", 
    subject: "", 
    message: "", 
    terms: false
};

export const ContactForm = (props: ContentContactFormState): JSX.Element =>
{
    const dispatch = useDispatch();
    const email = useSelector((state: ApplicationState) => state.applicationEmail);
    const error = useSelector((state: ApplicationState) => state.applicationError);

    const hasNotStarted = email?.status === OperationStatus.notStarted;
    const hasFinished = email?.status === OperationStatus.hasFinished;
    const hasError = error?.errorMessage === RECEIVED_ERROR_MESSAGE;

    const [form, setForm] = React.useState(formDefault);   
    const [hasProgress, setHasProgress] = React.useState(false);

    const showSuccess = (text: string) => dispatch(ApplicationDialogAction.raise(SuccessMessage(CONTACT_FORM, text)));
    const showWarning = (text: string)=> dispatch(ApplicationDialogAction.raise(WarningMessage(CONTACT_FORM, text)));

    const clearForm = React.useCallback(() => 
    {
        if (!hasProgress) return;
        setHasProgress(false);
        dispatch(ApplicationMessageAction.clear());
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
            dispatch(ApplicationMessageAction.send(
            {
                firstName: form.firstName,
                lastName: form.lastName,
                userEmail: form.email,
                emailFrom: form.email,
                emailTos: [form.email],
                subject: form.subject,
                message: form.message
            }));

            return;
        }

        if (hasFinished) 
        {
            clearForm();
            setForm(formDefault);
            showSuccess(MESSAGE_OUT_SUCCESS);        
        }
    }, 
    [ hasProgress, hasError, hasNotStarted, hasFinished ]);

    const keyHandler = React.useCallback((event: ReactKeyboardEvent) => 
    {
        if (event.code === "Enter")
        {
            buttonHandler();
        }
    }, []);

    const formHandler = React.useCallback((event: ReactChangeEvent) => 
    {
        if (event.currentTarget.name !== "terms")
        {
            setForm({ ...form, [event.currentTarget.name]: event.currentTarget.value});    
            return;
        }

        setForm({ ...form, [event.currentTarget.name]: event.currentTarget.checked});
    }, 
    [ form ]);

    const buttonHandler = React.useCallback(() => 
    {
        const result = ValidateContactForm( 
        { 
            firstName: form.firstName,
            lastName: form.lastName, 
            email: form.email, 
            subject: form.subject, 
            message: form.message, 
            terms: form.terms 
        });

        if (!Validate.isDefined(result))
        {
            setHasProgress(true);
            return;
        }

        showWarning(GetTextWarning({ object: result, template: MESSAGE_OUT_WARNING }));
    }, 
    [ form ]);

    return (<ContactFormView
        isLoading={props.isLoading}
        caption={props.content?.caption}
        text={props.content?.text}
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
        buttonText={props.content?.button}
        consent={props.content?.consent}
        labelFirstName={props.content?.labelFirstName}
        labelLastName={props.content?.labelLastName}
        labelEmail={props.content?.labelEmail}
        labelSubject={props.content?.labelSubject}
        labelMessage={props.content?.labelMessage}
    />);
}
