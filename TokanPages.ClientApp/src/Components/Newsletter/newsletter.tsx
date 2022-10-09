import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { IApplicationState } from "../../Store/Configuration";
import { IContentNewsletter } from "../../Store/States";
import { SubscriberAddAction } from "../../Store/Actions";
import { ApplicationDialogAction } from "../../Store/Actions";
import { OperationStatus } from "../../Shared/enums";
import { GetTextWarning, SuccessMessage, WarningMessage } from "../../Shared/Services/Utilities";
import { ValidateEmailForm } from "../../Shared/Services/FormValidation";
import { NewsletterView } from "./View/newsletterView";
import Validate from "validate.js";

import { 
    NEWSLETTER, 
    NEWSLETTER_SUCCESS, 
    NEWSLETTER_WARNING, 
    RECEIVED_ERROR_MESSAGE 
} from "../../Shared/constants";

export const Newsletter = (props: IContentNewsletter): JSX.Element =>
{
    const dispatch = useDispatch();
    const appState = useSelector((state: IApplicationState) => state.subscriberAdd);
    const appError = useSelector((state: IApplicationState) => state.applicationError);

    const [form, setForm] = React.useState({email: ""});
    const [progress, setProgress] = React.useState(false);

    const showSuccess = (text: string) => dispatch(ApplicationDialogAction.raise(SuccessMessage(NEWSLETTER, text)));
    const showWarning = (text: string) => dispatch(ApplicationDialogAction.raise(WarningMessage(NEWSLETTER, text)));

    const clearForm = React.useCallback(() => 
    {
        if (!progress) return;
        setProgress(false);
        dispatch(SubscriberAddAction.clear());
    }, 
    [ progress ]);

    React.useEffect(() => 
    {
        if (appError?.defaultErrorMessage === RECEIVED_ERROR_MESSAGE)
        {
            clearForm();
            return;
        }

        switch(appState?.operationStatus)
        {
            case OperationStatus.notStarted: 
                if (progress) 
                {
                    dispatch(SubscriberAddAction.add({ email: form.email }));
                }
            break;
        
            case OperationStatus.hasFinished: 
                clearForm();
                setForm({email: ""});
                showSuccess(NEWSLETTER_SUCCESS);
            break;
        }
    }, 
    [ progress, appError?.defaultErrorMessage, appState?.operationStatus, 
    OperationStatus.notStarted, OperationStatus.hasFinished ]);

    const formHandler = (event: React.ChangeEvent<HTMLInputElement>) => 
    { 
        setForm({ ...form, [event.currentTarget.name]: event.currentTarget.value }); 
    };
    
    const buttonHandler = () =>
    {
        let results = ValidateEmailForm({ email: form.email });

        if (!Validate.isDefined(results))
        {
            setProgress(true);
            return;
        }

        showWarning(GetTextWarning({ object: results, template: NEWSLETTER_WARNING }));
    };

    return (<NewsletterView bind=
    {{
        isLoading: props.isLoading,
        caption: props.content?.caption,
        text: props.content?.text,
        formHandler: formHandler,
        email: form.email,
        buttonHandler: buttonHandler,
        progress: progress,
        buttonText: props.content?.button,
        labelEmail: props.content?.labelEmail
    }}/>);
}
