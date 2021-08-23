import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { IApplicationState } from "../../Redux/applicationState";
import { ActionCreators as SubscriberAction } from "../../Redux/Actions/Subscribers/removeSubscriberAction";
import { ActionCreators as DialogAction } from "../../Redux/Actions/raiseDialogAction";
import { IGetUnsubscribeContent } from "../../Redux/States/Content/getUnsubscribeContentState";
import { OperationStatus } from "../../Shared/enums";
import SuccessMessage from "../../Shared/Components/ApplicationDialogBox/Helpers/successMessage";
import { RECEIVED_ERROR_MESSAGE, REMOVE_SUBSCRIBER, NEWSLETTER_SUCCESS } from "../../Shared/constants";
import { IRemoveSubscriberDto } from "../../Api/Models";
import UnsubscribeView from "./unsubscribeView";

interface IGetUnsubscribeContentExtended extends IGetUnsubscribeContent
{
    id: string;
}

const Unsubscribe = (props: IGetUnsubscribeContentExtended): JSX.Element =>
{
    const contentPre = 
    { 
        caption: props.content?.contentPre.caption,
        text1:   props.content?.contentPre.text1, 
        text2:   props.content?.contentPre.text2, 
        text3:   props.content?.contentPre.text3, 
        button:  props.content?.contentPre.button
    };

    const contentPost = 
    {
        caption: props.content?.contentPost.caption,
        text1:   props.content?.contentPost.text1, 
        text2:   props.content?.contentPost.text2, 
        text3:   props.content?.contentPost.text3, 
        button:  props.content?.contentPost.button
    };

    const dispatch = useDispatch();
    const removeSubscriberState = useSelector((state: IApplicationState) => state).removeSubscriber;
    const raiseErrorState = useSelector((state: IApplicationState) => state.raiseError);

    const [content, setContent] = React.useState(contentPre);
    const [buttonState, setButtonState] = React.useState(true);
    const [progress, setProgress] = React.useState(false);

    const showSuccess = React.useCallback((text: string) => dispatch(DialogAction.raiseDialog(SuccessMessage(REMOVE_SUBSCRIBER, text))), [ dispatch ]);
    const removeSubscriber = React.useCallback((payload: IRemoveSubscriberDto) => dispatch(SubscriberAction.removeSubscriber(payload)), [ dispatch ]);

    const clearForm = React.useCallback(() => 
    {
        setProgress(false);
        setButtonState(true);
        setContent(contentPost);
    }, [ contentPost ]);

    const callRemoveSubscriber = React.useCallback(() => 
    {
        switch(removeSubscriberState?.operationStatus)
        {
            case OperationStatus.inProgress:
                if (raiseErrorState?.defaultErrorMessage === RECEIVED_ERROR_MESSAGE)
                    clearForm();
            break;
            
            case OperationStatus.notStarted:
                if (progress)
                    removeSubscriber({ id: props.id });
            break;

            case OperationStatus.hasFinished:
                clearForm();
                showSuccess(NEWSLETTER_SUCCESS);
            break;
        }
    }, [ removeSubscriber, removeSubscriberState, progress, props.id, showSuccess, clearForm, raiseErrorState ]);

    React.useEffect(() => callRemoveSubscriber(), [ callRemoveSubscriber ]);

    const buttonHandler = () =>
    {
        if (props.id == null)
            return;

        setProgress(true);
    };

    return (<UnsubscribeView bind=
    {{
        isLoading: props.isLoading,
        caption: content.caption,
        text1: content.text1,
        text2: content.text2,
        text3: content.text3,
        buttonHandler: buttonHandler,
        buttonState: buttonState,
        progress: progress,
        buttonText: content.button
    }}/>);
}

export default Unsubscribe;
