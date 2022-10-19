import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { IApplicationState } from "../../Store/Configuration";
import { SubscriberRemoveAction } from "../../Store/Actions";
import { IContentUnsubscribe } from "../../Store/States";
import { OperationStatus } from "../../Shared/enums";
import { RECEIVED_ERROR_MESSAGE } from "../../Shared/constants";
import { IContent } from "../../Api/Models";
import { UnsubscribeView } from "./View/unsubscribeView";

interface IGetUnsubscribeContentExtended extends IContentUnsubscribe
{
    id: string;
}

export const Unsubscribe = (props: IGetUnsubscribeContentExtended): JSX.Element =>
{
    const contentPre: IContent = props.content?.contentPre;
    const contentPost: IContent = props.content?.contentPost;

    const dispatch = useDispatch();
    const appState = useSelector((state: IApplicationState) => state.subscriberRemove);
    const appError = useSelector((state: IApplicationState) => state.applicationError);

    const [isRemoved, setIsRemoved] = React.useState(false);
    const [buttonState, setButtonState] = React.useState(true);
    const [progress, setProgress] = React.useState(false);

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
        if (appError?.defaultErrorMessage === RECEIVED_ERROR_MESSAGE)
        {
            clearForm();
            return;
        }

        switch(appState?.status)
        {
            case OperationStatus.notStarted:
                if (progress) 
                {
                    dispatch(SubscriberRemoveAction.remove({ id: props.id }));
                }
            break;

            case OperationStatus.hasFinished:
                setIsRemoved(true);
                setButtonState(false);
                setProgress(false);                        
            break;
        }
    }, 
    [ progress, appError?.defaultErrorMessage, appState?.status, 
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
