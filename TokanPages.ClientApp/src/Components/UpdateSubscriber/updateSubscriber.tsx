import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { IApplicationState } from "../../Store/Configuration";
import { IContentUpdateSubscriber } from "../../Store/States";
import { OperationStatus } from "../../Shared/enums";
import { ValidateEmailForm } from "../../Shared/Services/FormValidation";
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

interface IGetUpdateSubscriberContentExtended extends IContentUpdateSubscriber
{
    id: string;
}

export const UpdateSubscriber = (props: IGetUpdateSubscriberContentExtended): JSX.Element =>
{
    const buttonDefaultState = props.id === null ? false : true;
    const dispatch = useDispatch();
    const update = useSelector((state: IApplicationState) => state.subscriberUpdate);
    const error = useSelector((state: IApplicationState) => state.applicationError);

    const hasNotStarted = update?.status === OperationStatus.notStarted;
    const hasFinished = update?.status === OperationStatus.hasFinished;
    const hasError = error?.errorMessage === RECEIVED_ERROR_MESSAGE;

    const [form, setForm] = React.useState({email: ""});
    const [buttonState, setButtonState] = React.useState(buttonDefaultState);
    const [progress, setProgress] = React.useState(false);

    const showSuccess = (text: string) => dispatch(ApplicationDialogAction.raise(SuccessMessage(UPDATE_SUBSCRIBER, text)));
    const showWarning = (text: string)=> dispatch(ApplicationDialogAction.raise(WarningMessage(UPDATE_SUBSCRIBER, text)));

    const clearForm = React.useCallback(() => 
    { 
        if (!progress) return;
        setProgress(false);
        setButtonState(true);
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
    [ progress, hasError, hasNotStarted, hasFinished ]);

    const formHandler = (event: React.ChangeEvent<HTMLInputElement>) => 
    {
        setForm({ ...form, [event.currentTarget.name]: event.currentTarget.value });
    };

    const buttonHandler = () =>
    {
        if (props.id == null) 
            return;

        let validationResult = ValidateEmailForm({ email: form.email });

        if (!Validate.isDefined(validationResult))
        {
            setButtonState(false);
            setProgress(true);
            return;
        }

        showWarning(GetTextWarning({ object: validationResult, template: NEWSLETTER_WARNING }));
    };

    return (<UpdateSubscriberView bind=
    {{
        isLoading: props.isLoading,
        caption: props.content?.caption,
        formHandler: formHandler,
        email: form.email,
        buttonHandler: buttonHandler,
        buttonState: buttonState,
        progress: progress,
        buttonText: props.content?.button,
        labelEmail: props.content.labelEmail
    }}/>);
}
