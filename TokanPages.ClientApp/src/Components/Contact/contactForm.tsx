import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import Validate from "validate.js";
import { ActionCreators as MessageAction } from "../../Redux/Actions/Mailer/sendMessageAction";
import { ActionCreators as DialogAction } from "../../Redux/Actions/raiseDialogAction";
import { IApplicationState } from "../../Redux/applicationState";
import { IGetContactFormContent } from "../../Redux/States/Content/getContactFormContentState";
import { OperationStatus } from "../../Shared/enums";
import { IValidateContactForm, ValidateContactForm } from "../../Shared/validate";
import { ProduceWarningText } from "../../Shared/textWrappers";
import SuccessMessage from "../../Shared/Components/ApplicationDialogBox/Helpers/successMessage";
import WarningMessage from "../../Shared/Components/ApplicationDialogBox/Helpers/warningMessage";
import { CONTACT_FORM, MESSAGE_OUT_SUCCESS, MESSAGE_OUT_WARNING, RECEIVED_ERROR_MESSAGE } from "../../Shared/constants";
import { ISendMessageDto } from "../../Api/Models";
import ContactFormView from "./contactFormView";

const formDefaultValues: IValidateContactForm =
{
    firstName: "", 
    lastName: "", 
    email: "", 
    subject: "", 
    message: "", 
    terms: false
};

const ContactForm = (props: IGetContactFormContent): JSX.Element =>
{
    const dispatch = useDispatch();
    const sendMessageState = useSelector((state: IApplicationState) => state.sendMessage);
    const raiseErrorState = useSelector((state: IApplicationState) => state.raiseError);

    const [form, setForm] = React.useState(formDefaultValues);   
    const [progress, setProgress] = React.useState(false);

    const showSuccess = React.useCallback((text: string) => dispatch(DialogAction.raiseDialog(SuccessMessage(CONTACT_FORM, text))), [ dispatch ]);
    const showWarning = React.useCallback((text: string)=> dispatch(DialogAction.raiseDialog(WarningMessage(CONTACT_FORM, text))), [ dispatch ]);
    const sendMessage = React.useCallback((payload: ISendMessageDto) => dispatch(MessageAction.sendMessage(payload)), [ dispatch ]);
    const sendMessageClear = React.useCallback(() => dispatch(MessageAction.sendMessageClear()), [ dispatch ]);

    const clearForm = React.useCallback(() => 
    {
        if (!progress) return;
        setProgress(false);
        sendMessageClear();
    }, 
    [ progress, sendMessageClear ]);

    React.useEffect(() => 
    {
        if (raiseErrorState?.defaultErrorMessage === RECEIVED_ERROR_MESSAGE)
        {
            clearForm();
            return;
        }

        switch(sendMessageState?.operationStatus)
        {
            case OperationStatus.notStarted:
                if (progress) sendMessage(
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
    [ progress, raiseErrorState?.defaultErrorMessage, sendMessageState?.operationStatus, 
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

        showWarning(ProduceWarningText(validationResult, MESSAGE_OUT_WARNING));
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
        consent: props.content.consent
    }}/>);
}

export default ContactForm;
