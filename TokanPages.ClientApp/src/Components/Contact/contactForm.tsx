import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { IApplicationState } from "../../Store/Configuration";
import { ApplicationDialogAction, ApplicationMessageAction } from "../../Store/Actions";
import { IContentContactForm } from "../../Store/States";
import { OperationStatus } from "../../Shared/enums";
import { IValidateContactForm, ValidateContactForm } from "../../Shared/Services/FormValidation";
import { GetTextWarning, SuccessMessage, WarningMessage } from "../../Shared/Services/Utilities";
import { ContactFormView } from "./View/contactFormView";
import Validate from "validate.js";

import { 
    CONTACT_FORM, 
    MESSAGE_OUT_SUCCESS, 
    MESSAGE_OUT_WARNING, 
    RECEIVED_ERROR_MESSAGE 
} from "../../Shared/constants";

const formDefaultValues: IValidateContactForm =
{
    firstName: "", 
    lastName: "", 
    email: "", 
    subject: "", 
    message: "", 
    terms: false
};

export const ContactForm = (props: IContentContactForm): JSX.Element =>
{
    const dispatch = useDispatch();
    const appState = useSelector((state: IApplicationState) => state.applicationMessage);
    const appError = useSelector((state: IApplicationState) => state.applicationError);

    const [form, setForm] = React.useState(formDefaultValues);   
    const [progress, setProgress] = React.useState(false);

    const showSuccess = (text: string) => dispatch(ApplicationDialogAction.raise(SuccessMessage(CONTACT_FORM, text)));
    const showWarning = (text: string)=> dispatch(ApplicationDialogAction.raise(WarningMessage(CONTACT_FORM, text)));

    const clearForm = React.useCallback(() => 
    {
        if (!progress) return;
        setProgress(false);
        dispatch(ApplicationMessageAction.clear());
    }, 
    [ progress ]);

    React.useEffect(() => 
    {
        if (appError?.errorMessage === RECEIVED_ERROR_MESSAGE)
        {
            clearForm();
            return;
        }

        switch(appState?.status)
        {
            case OperationStatus.notStarted:
                if (progress) dispatch(ApplicationMessageAction.send(
                {
                    firstName: form.firstName,
                    lastName: form.lastName,
                    userEmail: form.email,
                    emailFrom: form.email,
                    emailTos: [form.email],
                    subject: form.subject,
                    message: form.message
                }));
            break;

            case OperationStatus.hasFinished:
                clearForm();
                setForm(formDefaultValues);
                showSuccess(MESSAGE_OUT_SUCCESS);
            break;
        }
    }, 
    [ progress, appError?.errorMessage, appState?.status, 
    OperationStatus.notStarted, OperationStatus.hasFinished ]);

    const formHandler = (event: React.ChangeEvent<HTMLInputElement>) => 
    {
        if (event.currentTarget.name !== "terms")
        {
            setForm({ ...form, [event.currentTarget.name]: event.currentTarget.value});    
            return;
        }

        setForm({ ...form, [event.currentTarget.name]: event.currentTarget.checked});
    };

    const buttonHandler = async () => 
    {
        let validationResult = ValidateContactForm( 
        { 
            firstName: form.firstName,
            lastName: form.lastName, 
            email: form.email, 
            subject: form.subject, 
            message: form.message, 
            terms: form.terms 
        });

        if (!Validate.isDefined(validationResult))
        {
            setProgress(true);
            return;
        }

        showWarning(GetTextWarning({ object: validationResult, template: MESSAGE_OUT_WARNING }));
    };

    return (<ContactFormView bind=
    {{
        isLoading: props.isLoading,
        caption: props.content?.caption,
        text: props.content?.text,
        formHandler: formHandler,
        firstName: form.firstName,
        lastName: form.lastName,
        email: form.email,
        subject: form.subject,
        message: form.message,
        terms: form.terms,
        buttonHandler: buttonHandler,
        progress: progress,
        buttonText: props.content?.button,
        consent: props.content?.consent,
        labelFirstName: props.content?.labelFirstName,
        labelLastName: props.content?.labelLastName,
        labelEmail: props.content?.labelEmail,
        labelSubject: props.content?.labelSubject,
        labelMessage: props.content?.labelMessage
    }}/>);
}
