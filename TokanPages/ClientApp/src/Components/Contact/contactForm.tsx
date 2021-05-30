import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import Validate from "validate.js";
import { ActionCreators } from "../../Redux/Actions/sendMessageAction";
import { IApplicationState } from "../../Redux/applicationState";
import { IGetContactFormContent } from "../../Redux/States/getContactFormContentState";
import { OperationStatus, IconType } from "../../Shared/enums";
import { ValidateContactForm } from "../../Shared/validate";
import { alertModalDefault } from "../../Shared/Components/AlertDialog/alertDialog";
import { MessageOutSuccess, MessageOutWarning, MessageOutError } from "../../Shared/textWrappers";
import { ISendMessageDto } from "../../Api/Models";
import ContactFormView from "./contactFormView";

interface IFormDefaultValues 
{
    firstName: string;
    lastName: string;
    email: string;
    subject: string;
    message: string; 
    terms: boolean;
}

const formDefaultValues: IFormDefaultValues =
{
    firstName: "", 
    lastName: "", 
    email: "", 
    subject: "", 
    message: "", 
    terms: false
};

export default function ContactForm(props: IGetContactFormContent)
{
    const [form, setForm] = React.useState(formDefaultValues);   
    const [modal, setModal] = React.useState(alertModalDefault);
    const [progress, setProgress] = React.useState(false);

    const showSuccess = (text: string) => setModal({ State: true, Title: "Contact Form", Message: text, Icon: IconType.info });
    const showWarning = (text: string) => setModal({ State: true, Title: "Warning", Message: text, Icon: IconType.warning });
    const showError = (text: string) => setModal({ State: true, Title: "Error", Message: text, Icon: IconType.error });

    const dispatch = useDispatch();
    const sendMessageState = useSelector((state: IApplicationState) => state.sendMessage);
    const sendMessage = React.useCallback((payload: ISendMessageDto) => dispatch(ActionCreators.sendMessage(payload)), [ dispatch ]);
    const sendMessageClear = React.useCallback(() => dispatch(ActionCreators.sendMessageClear()), [ dispatch ]);

    const clearForm = React.useCallback(() => 
    {
        setProgress(false);
        setForm(formDefaultValues);
        sendMessageClear();
    }, [ setProgress, setForm, sendMessageClear ]);

    React.useEffect(() => 
    { 
        switch(sendMessageState.operationStatus)
        {
            case OperationStatus.notStarted:
                if (progress)
                    sendMessage(
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
                showSuccess(MessageOutSuccess());
                clearForm();
            break;
            case OperationStatus.hasFailed:
                showError(MessageOutError(sendMessageState.attachedErrorObject));
                clearForm();
            break;
        }
    }, [ sendMessage, sendMessageState, clearForm, progress, form ]);

    const modalHandler = () => setModal({ ...modal, State: false});
    const formHandler = (event: React.ChangeEvent<HTMLInputElement>) => 
    {
        switch(event.currentTarget.name)
        {
            case "terms": 
                setForm({ ...form, [event.currentTarget.name]: event.currentTarget.checked});
            break;
            default: 
                setForm({ ...form, [event.currentTarget.name]: event.currentTarget.value});    
        }
    };

    const buttonHandler = async () => 
    {
        let validationResult = ValidateContactForm( 
        { 
            FirstName: form.firstName,
            LastName: form.lastName, 
            Email: form.email, 
            Subject: form.subject, 
            Message: form.message, 
            Terms: form.terms 
        });

        if (!Validate.isDefined(validationResult))
        {
            setProgress(true);
            return;
        }

        showWarning(MessageOutWarning(validationResult));
    };

    return (<ContactFormView bind={
    {
        modalState: modal.State,
        modalHandler: modalHandler,
        modalTitle: modal.Title,
        modalMessage: modal.Message,
        modalIcon: modal.Icon,
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
        buttonText: props.content?.button
    }}/>);
}
