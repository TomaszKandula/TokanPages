import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../Store/Configuration";
import { ContentUpdateSubscriberState } from "../../Store/States";
import { OperationStatus } from "../../Shared/enums";
import { ValidateEmailForm } from "../../Shared/Services/FormValidation";
import { ReactChangeEvent } from "../../Shared/types";
import { UpdateSubscriberView } from "./View/updateSubscriberView";
import Validate from "validate.js";

import { 
    ApplicationDialogAction, 
    SubscriberUpdateAction 
} from "../../Store/Actions";

import { 
    GetTextWarning, 
    SuccessMessage, 
    WarningMessage 
} from "../../Shared/Services/Utilities";

import { 
    NEWSLETTER_SUCCESS, 
    NEWSLETTER_WARNING, 
    RECEIVED_ERROR_MESSAGE, 
    UPDATE_SUBSCRIBER 
} from "../../Shared/constants";

interface Properties extends ContentUpdateSubscriberState
{
    id: string;
}

export const UpdateSubscriber = (props: Properties): JSX.Element =>
{
    const hasButtonVisible = props.id === null ? false : true;
    const dispatch = useDispatch();
    const update = useSelector((state: ApplicationState) => state.subscriberUpdate);
    const error = useSelector((state: ApplicationState) => state.applicationError);

    const hasNotStarted = update?.status === OperationStatus.notStarted;
    const hasFinished = update?.status === OperationStatus.hasFinished;
    const hasError = error?.errorMessage === RECEIVED_ERROR_MESSAGE;

    const [form, setForm] = React.useState({email: ""});
    const [hasButton, setHasButton] = React.useState(hasButtonVisible);
    const [hasProgress, setHasProgress] = React.useState(false);

    const showSuccess = (text: string) => dispatch(ApplicationDialogAction.raise(SuccessMessage(UPDATE_SUBSCRIBER, text)));
    const showWarning = (text: string)=> dispatch(ApplicationDialogAction.raise(WarningMessage(UPDATE_SUBSCRIBER, text)));

    const clearForm = React.useCallback(() => 
    { 
        if (!hasProgress) return;
        setHasProgress(false);
        setHasButton(true);
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
            dispatch(SubscriberUpdateAction.update(
            {
                id: props.id, 
                email: form.email, 
                isActivated: true, 
                count: 0 
            }));
            
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

    const formHandler = React.useCallback((event: ReactChangeEvent) => 
    {
        setForm({ ...form, [event.currentTarget.name]: event.currentTarget.value });
    }, [ form ]);

    const buttonHandler = React.useCallback(() =>
    {
        if (props.id == null) 
            return;

        let validationResult = ValidateEmailForm({ email: form.email });

        if (!Validate.isDefined(validationResult))
        {
            setHasButton(false);
            setHasProgress(true);
            return;
        }

        showWarning(GetTextWarning({ object: validationResult, template: NEWSLETTER_WARNING }));
    }, [ props.id, form ]);

    return (<UpdateSubscriberView
        isLoading={props.isLoading}
        caption={props.content?.caption}
        formHandler={formHandler}
        email={form.email}
        buttonHandler={buttonHandler}
        buttonState={hasButton}
        progress={hasProgress}
        buttonText={props.content?.button}
        labelEmail={props.content.labelEmail}
    />);
}
