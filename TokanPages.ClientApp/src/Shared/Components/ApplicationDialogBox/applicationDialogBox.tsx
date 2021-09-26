import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { IApplicationState } from "../../../Redux/applicationState";
import { ActionCreators } from "../../../Redux/Actions/raiseDialogAction";
import { IRaiseDialog } from "../../../Redux/States/raiseDialogState";
import { IconType } from "../../enums";
import Validate from "validate.js";
import ApplicationDialogBoxView from "./applicationDialogBoxView";

interface IDialogState extends IRaiseDialog
{
    state: boolean;
}

const DialogState: IDialogState = 
{
    state: false, 
    title:  "", 
    message: "", 
    icon: IconType.info
}

const ApplicationDialogBox = (): JSX.Element => 
{
    const dispatch = useDispatch();
    const [dialogState, setDialogState] = React.useState(DialogState);
    const raiseDialogState = useSelector((state: IApplicationState) => state.raiseDialog);
    
    const clearDialog = React.useCallback(() => 
    { 
        if (!dialogState.state && !Validate.isEmpty(dialogState.message))
        {
            dispatch(ActionCreators.clearDialog());
            setDialogState(DialogState);
        }

    }, [ dispatch, dialogState ]);
    
    const raiseDialog = React.useCallback(() => 
    {
        if (!Validate.isEmpty(raiseDialogState?.message))
        {
            setDialogState(
            { 
                state: true,
                title: raiseDialogState?.title,
                message: raiseDialogState?.message,
                icon: raiseDialogState?.icon
            });
        }
    }, 
    [ raiseDialogState ]);

    React.useEffect(() => raiseDialog(), [ raiseDialog ]);
    React.useEffect(() => clearDialog(), [ clearDialog ]);

    const onClickHandler = () => 
    {
        setDialogState({ ...dialogState, state: false });
    };

    return (<ApplicationDialogBoxView bind=
    {{
        state: dialogState.state,
        icon: dialogState.icon,
        title: dialogState.title,
        message: dialogState.message,
        closeHandler: onClickHandler
    }}/>);
}

export default ApplicationDialogBox;
