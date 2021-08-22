import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import Validate from "validate.js";
import { IApplicationState } from "../../Redux/applicationState";
import { ActionCreators as SubscriberAction } from "../../Redux/Actions/Subscribers/updateSubscriberAction";
import { ActionCreators as RaiseDialogAction } from "../../Redux/Actions/raiseDialogAction";
import { IGetUpdateSubscriberContent } from "../../Redux/States/Content/getUpdateSubscriberContentState";
import { OperationStatus } from "../../Shared/enums";
import { ValidateEmail } from "../../Shared/validate";
import { NewsletterSuccess, NewsletterWarning } from "../../Shared/textWrappers";
import SuccessMessage from "../../Shared/Components/ApplicationDialogBox/Helpers/successMessage";
import WarningMessage from "../../Shared/Components/ApplicationDialogBox/Helpers/warningMessage";
import { RECEIVED_ERROR_MESSAGE, UPDATE_SUBSCRIBER } from "../../Shared/constants";
import { IUpdateSubscriberDto } from "../../Api/Models";
import UpdateSubscriberView from "./updateSubscriberView";

interface IGetUpdateSubscriberContentExtended extends IGetUpdateSubscriberContent
{
    id: string;
}

export default function UpdateSubscriber(props: IGetUpdateSubscriberContentExtended)
{
    const dispatch = useDispatch();
    const updateSubscriberState = useSelector((state: IApplicationState) => state.updateSubscriber);
    const raiseErrorState = useSelector((state: IApplicationState) => state.raiseError);
    
    const [form, setForm] = React.useState({email: ""});
    const [buttonState, setButtonState] = React.useState(true);
    const [progress, setProgress] = React.useState(false);

    const showSuccess = React.useCallback((text: string) => dispatch(RaiseDialogAction.raiseDialog(SuccessMessage(UPDATE_SUBSCRIBER, text))), [ dispatch ]);
    const showWarning = React.useCallback((text: string)=> dispatch(RaiseDialogAction.raiseDialog(WarningMessage(UPDATE_SUBSCRIBER, text))), [ dispatch ]);
    const updateSubscriber = React.useCallback((payload: IUpdateSubscriberDto) => dispatch(SubscriberAction.updateSubscriber(payload)), [ dispatch ]);
    
    const clearForm = React.useCallback(() => 
    { 
        setProgress(false);
        setButtonState(true);
        setForm({email: ""});
    }, [  ]);

    const callUpdateSubscriber = React.useCallback(() => 
    {
        switch(updateSubscriberState?.operationStatus)
        {
            case OperationStatus.inProgress:
                if (raiseErrorState?.defaultErrorMessage === RECEIVED_ERROR_MESSAGE)
                    clearForm();
            break;

            case OperationStatus.notStarted:
                if (progress)
                    updateSubscriber(
                    { 
                        id: props.id, 
                        email: form.email, 
                        isActivated: true, 
                        count: 0 
                    });
            break;

            case OperationStatus.hasFinished:
                clearForm();
                showSuccess(NewsletterSuccess());
            break;
        }           
    }, [ updateSubscriber, updateSubscriberState, progress, form, props.id, showSuccess, clearForm, raiseErrorState ]);

    React.useEffect(() => callUpdateSubscriber(), [ callUpdateSubscriber ]);

    const formHandler = (event: React.ChangeEvent<HTMLInputElement>) => 
    {
        setForm({ ...form, [event.currentTarget.name]: event.currentTarget.value });
    };

    const buttonHandler = () =>
    {
        if (props.id == null) 
            return;

        let validationResult = ValidateEmail(form.email);

        if (!Validate.isDefined(validationResult))
        {
            setButtonState(false);
            setProgress(true);
            return;
        }

        showWarning(NewsletterWarning(validationResult));
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
        buttonText: props.content?.button
    }}/>);
}
