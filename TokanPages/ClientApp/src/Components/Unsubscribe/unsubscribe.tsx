import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { IApplicationState } from "../../Redux/applicationState";
import { ActionCreators } from "../../Redux/Actions/removeSubscriberAction";
import { IGetUnsubscribeContent } from "../../Redux/States/getUnsubscribeContentState";
import { IconType, OperationStatus } from "../../Shared/enums";
import { alertModalDefault } from "../../Shared/Components/AlertDialog/alertDialog";
import { NewsletterError, NewsletterSuccess } from "../../Shared/textWrappers";
import { IRemoveSubscriberDto } from "../../Api/Models";
import UnsubscribeView from "./unsubscribeView";

interface IGetUnsubscribeContentExtended extends IGetUnsubscribeContent
{
    id: string;
}

export default function Unsubscribe(props: IGetUnsubscribeContentExtended)
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

    const [content, setContent] = React.useState(contentPre);
    const [buttonState, setButtonState] = React.useState(true);
    const [progress, setProgress] = React.useState(false);
    const [modal, setModal] = React.useState(alertModalDefault);

    const showSuccess = (text: string) => setModal({ State: true, Title: "Remove subscriber", Message: text, Icon: IconType.info });
    const showError = (text: string) => setModal({ State: true, Title: "Error", Message: text, Icon: IconType.error });
    
    const dispatch = useDispatch();
    const removeSubscriberState = useSelector((state: IApplicationState) => state).removeSubscriber;
    const removeSubscriber = React.useCallback((payload: IRemoveSubscriberDto) => dispatch(ActionCreators.removeSubscriber(payload)), [ dispatch ]);

    const executeRemoveSubscriber = React.useCallback(() => 
    {
        switch(removeSubscriberState.operationStatus)
        {
            case OperationStatus.notStarted:
                if (progress)
                    removeSubscriber({ id: props.id });
            break;

            case OperationStatus.hasFinished:
                setProgress(false);
                setButtonState(true);
                setContent(contentPost);
                showSuccess(NewsletterSuccess());
            break;

            case OperationStatus.hasFailed:
                showError(NewsletterError(removeSubscriberState.attachedErrorObject));
            break;
        }
    }, 
    [ removeSubscriber, removeSubscriberState, progress, props.id, contentPost ]);

    React.useEffect(() => executeRemoveSubscriber(), [ executeRemoveSubscriber ]);

    const modalHandler = () => 
    { 
        setModal(alertModalDefault); 
    };

    const buttonHandler = () =>
    {
        if (props.id == null)
            return;

        setProgress(true);
    };

    return (<UnsubscribeView bind=
    {{
        modalState: modal.State,
        modalHandler: modalHandler,
        modalTitle: modal.Title,
        modalMessage: modal.Message,
        modalIcon: modal.Icon,
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
