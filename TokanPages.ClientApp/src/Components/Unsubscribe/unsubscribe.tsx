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
    const remove = useSelector((state: IApplicationState) => state.subscriberRemove);
    const error = useSelector((state: IApplicationState) => state.applicationError);

    const hasNotStarted = remove?.status === OperationStatus.notStarted;
    const hasFinished = remove?.status === OperationStatus.hasFinished;
    const hasError = error?.errorMessage === RECEIVED_ERROR_MESSAGE;

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
        if (hasError)
        {
            clearForm();
            return;
        }

        if (hasNotStarted && progress)
        {
            dispatch(SubscriberRemoveAction.remove({ id: props.id }));
            return;
        }

        if (hasFinished)
        {
            setIsRemoved(true);
            setButtonState(false);
            setProgress(false);                        
        }
    }, 
    [ progress, hasError, hasNotStarted, hasFinished ]);

    const buttonHandler = () =>
    {
        if (props.id == null) return;
        setProgress(true);
    };

    return (<UnsubscribeView
        isLoading={props.isLoading}
        contentPre={contentPre}
        contentPost={contentPost}
        buttonHandler={buttonHandler}
        buttonState={buttonState}
        progress={progress}
        isRemoved={isRemoved}
    />);
}
