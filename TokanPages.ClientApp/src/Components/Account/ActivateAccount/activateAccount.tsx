import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useHistory } from "react-router-dom";
import { IApplicationState } from "../../../Store/Configuration";
import { UserActivateAction } from "../../../Store/Actions";
import { IGetActivateAccountContent } from "../../../Store/States";
import { RECEIVED_ERROR_MESSAGE } from "../../../Shared/constants";
import { OperationStatus } from "../../../Shared/enums";
import { IActivateUserDto } from "../../../Api/Models";
import { ActivateAccountView } from "./View/activateAccountView";

interface IGetActivateAccountContentExtended extends IGetActivateAccountContent
{
    id: string;
}

export const ActivateAccount = (props: IGetActivateAccountContentExtended): JSX.Element => 
{
    const onProcessing = 
    { 
        type: props.content?.onProcessing.type,
        caption: props.content?.onProcessing.caption,
        text1: props.content?.onProcessing.text1, 
        text2: props.content?.onProcessing.text2, 
        button: props.content?.onProcessing.button
    };

    const onSuccess = 
    {
        type: props.content?.onSuccess.type,
        caption: props.content?.onSuccess.caption,
        text1: props.content?.onSuccess.text1, 
        text2: props.content?.onSuccess.text2, 
        button: props.content?.onSuccess.button
    };

    const onError = 
    {
        type: props.content?.onError.type,
        caption: props.content?.onError.caption,
        text1: props.content?.onError.text1, 
        text2: props.content?.onError.text2, 
        button: props.content?.onError.button
    };

    const dispatch = useDispatch();
    const history = useHistory();

    const activateAccountState = useSelector((state: IApplicationState) => state.activateAccount);
    const raiseErrorState = useSelector((state: IApplicationState) => state.raiseError);
    const activateAccount = React.useCallback((payload: IActivateUserDto) => dispatch(UserActivateAction.activateAccount(payload)), [ dispatch ]);

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

        if (raiseErrorState?.defaultErrorMessage === RECEIVED_ERROR_MESSAGE)
        {
            setContent(onError);
            setProgress(false);
            setButtonDisabled(false);
            return;
        }

        switch(activateAccountState?.operationStatus)
        {
            case OperationStatus.notStarted:
                if (progress && !requested) 
                {
                    setRequested(true);
                    setTimeout(() => activateAccount(
                    { 
                        activationId: props.id 
                    }), 
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
    [ content.type, props.id, progress, requested, activateAccount, 
        activateAccountState, raiseErrorState, onProcessing, onSuccess, onError ]);
    
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
