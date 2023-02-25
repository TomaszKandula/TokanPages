import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ReactChangeEvent, ReactKeyboardEvent } from "../../Shared/types";
import { ApplicationState } from "../../Store/Configuration";
import { ContentNewsletterState } from "../../Store/States";
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

export const Newsletter = (props: ContentNewsletterState): JSX.Element =>
{
    const dispatch = useDispatch();
    const add = useSelector((state: ApplicationState) => state.subscriberAdd);
    const error = useSelector((state: ApplicationState) => state.applicationError);

    const hasNotStarted = add?.status === OperationStatus.notStarted;
    const hasFinished = add?.status === OperationStatus.hasFinished;
    const hasError = error?.errorMessage === RECEIVED_ERROR_MESSAGE;

    const [form, setForm] = React.useState({email: ""});
    const [hasProgress, setHasProgress] = React.useState(false);

    const showSuccess = (text: string) => dispatch(ApplicationDialogAction.raise(SuccessMessage(NEWSLETTER, text)));
    const showWarning = (text: string) => dispatch(ApplicationDialogAction.raise(WarningMessage(NEWSLETTER, text)));

    const clearForm = React.useCallback(() => 
    {
        if (!hasProgress) return;
        setHasProgress(false);
        dispatch(SubscriberAddAction.clear());
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
        setForm({ ...form, [event.currentTarget.name]: event.currentTarget.value }); 
    }, 
    [ form ]);

    const buttonHandler = React.useCallback(() =>
    {
        const result = ValidateEmailForm({ email: form.email });

        if (!Validate.isDefined(result))
        {
            setHasProgress(true);
            return;
        }

        showWarning(GetTextWarning({ object: result, template: NEWSLETTER_WARNING }));
    }, 
    [ form ]);

    return (<NewsletterView
        isLoading={props.isLoading}
        caption={props.content?.caption}
        text={props.content?.text}
        keyHandler={keyHandler}
        formHandler={formHandler}
        email={form.email}
        buttonHandler={buttonHandler}
        progress={hasProgress}
        buttonText={props.content?.button}
        labelEmail={props.content?.labelEmail}
    />);
}
