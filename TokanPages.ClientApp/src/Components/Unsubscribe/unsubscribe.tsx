import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { IApplicationState } from "../../Store/Configuration";
import { SubscriberRemoveAction } from "../../Store/Actions";
import { IGetUnsubscribeContent } from "../../Store/States";
import { OperationStatus } from "../../Shared/enums";
import { RECEIVED_ERROR_MESSAGE } from "../../Shared/constants";
import { IContent } from "../../Api/Models";
import { IRemoveSubscriberDto } from "../../Api/Models";
import { UnsubscribeView } from "./View/unsubscribeView";

interface IGetUnsubscribeContentExtended extends IGetUnsubscribeContent
{
    id: string;
}

export const Unsubscribe = (props: IGetUnsubscribeContentExtended): JSX.Element =>
{
    const contentPre: IContent = 
    { 
        caption: props.content?.contentPre.caption,
        text1: props.content?.contentPre.text1, 
        text2: props.content?.contentPre.text2, 
        text3: props.content?.contentPre.text3, 
        button: props.content?.contentPre.button
    };

    const contentPost: IContent = 
    {
        caption: props.content?.contentPost.caption,
        text1: props.content?.contentPost.text1, 
        text2: props.content?.contentPost.text2, 
        text3: props.content?.contentPost.text3, 
        button: props.content?.contentPost.button
    };

    const dispatch = useDispatch();
    const removeSubscriberState = useSelector((state: IApplicationState) => state.removeSubscriber);
    const raiseErrorState = useSelector((state: IApplicationState) => state.raiseError);

    const [isRemoved, setIsRemoved] = React.useState(false);
    const [buttonState, setButtonState] = React.useState(true);
    const [progress, setProgress] = React.useState(false);

    const removeSubscriber = React.useCallback((payload: IRemoveSubscriberDto) => dispatch(SubscriberRemoveAction.removeSubscriber(payload)), [ dispatch ]);

    const clearForm = React.useCallback(() => 
    {
        if (!progress) return;
        setIsRemoved(false);
        setButtonState(true);
        setProgress(false);
    }, 
    [ progress, contentPost ]);

    React.useEffect(() => 
    {
        if (raiseErrorState?.defaultErrorMessage === RECEIVED_ERROR_MESSAGE)
        {
            clearForm();
            return;
        }

        switch(removeSubscriberState?.operationStatus)
        {
            case OperationStatus.notStarted:
                if (progress) removeSubscriber({ id: props.id });
            break;

            case OperationStatus.hasFinished:
                setIsRemoved(true);
                setButtonState(false);
                setProgress(false);                        
            break;
        }
    }, 
    [ progress, raiseErrorState?.defaultErrorMessage, removeSubscriberState?.operationStatus, 
        OperationStatus.notStarted, OperationStatus.hasFinished ]);

    const buttonHandler = () =>
    {
        if (props.id == null) return;
        setProgress(true);
    };

    return (<UnsubscribeView bind=
    {{
        isLoading: props.isLoading,
        contentPre: contentPre,
        contentPost: contentPost,
        buttonHandler: buttonHandler,
        buttonState: buttonState,
        progress: progress,
        isRemoved: isRemoved
    }}/>);
}
