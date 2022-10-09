import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import Validate from "validate.js";
import { IApplicationState } from "../../Store/Configuration";
import { ApplicationDialogAction, ApplicationMessageAction } from "../../Store/Actions";
import { IContentContactForm } from "../../Store/States";
import { OperationStatus } from "../../Shared/enums";
import { IValidateContactForm, ValidateContactForm } from "../../Shared/Services/FormValidation";
import { GetTextWarning, SuccessMessage, WarningMessage } from "../../Shared/Services/Utilities";
import { ISendMessageDto } from "../../Api/Models";
import { ContactFormView } from "./View/contactFormView";

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
    const state = useSelector((state: IApplicationState) => state.applicationMessage);
    const error = useSelector((state: IApplicationState) => state.applicationError);

    const [form, setForm] = React.useState(formDefaultValues);   
    const [progress, setProgress] = React.useState(false);

    const showSuccess = React.useCallback((text: string) => dispatch(ApplicationDialogAction.raise(SuccessMessage(CONTACT_FORM, text))), [ dispatch ]);
    const showWarning = React.useCallback((text: string)=> dispatch(ApplicationDialogAction.raise(WarningMessage(CONTACT_FORM, text))), [ dispatch ]);
    const message = React.useCallback((payload: ISendMessageDto) => dispatch(ApplicationMessageAction.send(payload)), [ dispatch ]);
    const clear = React.useCallback(() => dispatch(ApplicationMessageAction.clear()), [ dispatch ]);

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
                if (progress) message(
                {
                    firstName: form.firstName,
                    lastName: form.lastName,
                    userEmail: form.email,
                    emailFrom: form.email,
                    emailTos: [form.email],
                    subject: form.subject,
                    message: form.message
                });
            break;

            case OperationStatus.hasFinished:
                clearForm();
                setForm(formDefaultValues);
                showSuccess(MESSAGE_OUT_SUCCESS);
            break;
        }
    }, 
    [ progress, error?.defaultErrorMessage, state?.operationStatus, 
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
