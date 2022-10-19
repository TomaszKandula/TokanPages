import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useHistory } from "react-router-dom";
import { IApplicationState } from "../../../Store/Configuration";
import { UserActivateAction } from "../../../Store/Actions";
import { IContentActivateAccount } from "../../../Store/States";
import { RECEIVED_ERROR_MESSAGE } from "../../../Shared/constants";
import { OperationStatus } from "../../../Shared/enums";
import { ActivateAccountView } from "./View/activateAccountView";

interface IGetActivateAccountContentExtended extends IContentActivateAccount
{
    id: string;
}

export const ActivateAccount = (props: IGetActivateAccountContentExtended): JSX.Element => 
{
    const onProcessing = props.content?.onProcessing;
    const onSuccess = props.content?.onSuccess;
    const onError = props.content?.onError;

    const dispatch = useDispatch();
    const history = useHistory();

    const appState = useSelector((state: IApplicationState) => state.userActivate);
    const appError = useSelector((state: IApplicationState) => state.applicationError);

    const [content, setContent] = React.useState(onProcessing);
    const [buttonDisabled, setButtonDisabled] = React.useState(true);
    const [progress, setProgress] = React.useState(true);
    const [requested, setRequested] = React.useState(false);
 
    React.useEffect(() => 
    {
        if (!progress) return;
        
        if (content.type === "Unset") 
        {
            setContent(onProcessing);
            return;
        }

        if (appError?.errorMessage === RECEIVED_ERROR_MESSAGE)
        {
            setContent(onError);
            setProgress(false);
            setButtonDisabled(false);
            return;
        }

        switch(appState?.status)
        {
            case OperationStatus.notStarted:
                if (progress && !requested) 
                {
                    setRequested(true);
                    setTimeout(() => dispatch(UserActivateAction.activate(
                    {
                        activationId: props.id 
                    })),
                    1500);
                }
            break;

            case OperationStatus.hasFinished:
                setContent(onSuccess);
                setProgress(false);
                setButtonDisabled(false);
            break;
        }
    }, 
    [ content.type, props.id, progress, requested, 
    appState, appError, onProcessing, onSuccess, onError ]);
 
    const buttonHandler = () =>
    {
        if (content.type === "Error")
        {
            setContent(onProcessing);
            setRequested(false);
            setProgress(true);
            setButtonDisabled(true);
            dispatch(UserActivateAction.clear())
        }
        
        if (content.type === "Success")
        {
            history.push("/");
        }
    };

    return (<ActivateAccountView bind=
    {{
        isLoading: props.isLoading,
        caption: content.caption,
        text1: content.text1,
        text2: content.text2,
        buttonHandler: buttonHandler,
        buttonDisabled: buttonDisabled,
        progress: progress,
        buttonText: content.button
    }}/>);
}
