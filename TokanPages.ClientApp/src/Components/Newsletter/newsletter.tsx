import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { IApplicationState } from "../../Store/Configuration";
import { IContentNewsletter } from "../../Store/States";
import { OperationStatus } from "../../Shared/enums";
import { ValidateEmailForm } from "../../Shared/Services/FormValidation";
import { NewsletterView } from "./View/newsletterView";
import Validate from "validate.js";

import { 
    SubscriberAddAction, 
    ApplicationDialogAction 
} from "../../Store/Actions";

import { 
    GetTextWarning, 
    SuccessMessage, 
    WarningMessage 
} from "../../Shared/Services/Utilities";

import { 
    NEWSLETTER, 
    NEWSLETTER_SUCCESS, 
    NEWSLETTER_WARNING, 
    RECEIVED_ERROR_MESSAGE 
} from "../../Shared/constants";

export const Newsletter = (props: IContentNewsletter): JSX.Element =>
{
    const dispatch = useDispatch();
    const add = useSelector((state: IApplicationState) => state.subscriberAdd);
    const error = useSelector((state: IApplicationState) => state.applicationError);

    const hasNotStarted = add?.status === OperationStatus.notStarted;
    const hasFinished = add?.status === OperationStatus.hasFinished;
    const hasError = error?.errorMessage === RECEIVED_ERROR_MESSAGE;

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
        if (hasError)
        {
            clearForm();
            return;
        }

        if (hasNotStarted && progress)
        {
            dispatch(SubscriberAddAction.add({ email: form.email }));
            return;
        }

        if (hasFinished)
        {
            clearForm();
            setForm({email: ""});
            showSuccess(NEWSLETTER_SUCCESS);
        }
    }, 
    [ progress, hasError, hasNotStarted, hasFinished ]);

    const keyHandler = (event: React.KeyboardEvent<HTMLInputElement>) => 
    {
        if (event.code === "Enter")
        {
            buttonHandler();
        }
    }

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
        keyHandler: keyHandler,
        formHandler: formHandler,
        email: form.email,
        buttonHandler: buttonHandler,
        progress: progress,
        buttonText: props.content?.button,
        labelEmail: props.content?.labelEmail
    }}/>);
}
