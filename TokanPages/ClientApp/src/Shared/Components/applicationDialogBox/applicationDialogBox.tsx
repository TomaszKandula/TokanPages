import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { IApplicationState } from "../../../Redux/applicationState";
import { ActionCreators } from "../../../Redux/Actions/raiseErrorAction";
import { IRaiseDialog } from "../../../Redux/States/raiseDialogState";
import { IconType } from "../../enums";
import ApplicationDialogBoxView from "./applicationDialogBoxView";

interface IDefaultDialogState extends IRaiseDialog
{
    state: boolean;
}

const DefaultDialogState: IDefaultDialogState = 
{
    state: false, 
    title:  "", 
    message: "", 
    icon: IconType.info
}

export default function ApplicationDialogBox() 
{
    const dispatch = useDispatch();
    const [dialogState, setDialogState] = React.useState(DefaultDialogState);
    const raiseDialogState = useSelector((state: IApplicationState) => state.raiseDialog);
    
    const clearDialog = React.useCallback(() => 
    { 
        if (!dialogState.state && dialogState.message !== "")
        {
            dispatch(ActionCreators.clearError());
            setDialogState(DefaultDialogState);
        }

    }, [ dispatch, dialogState ]);
    
    const raiseDialog = React.useCallback(() => 
    {
        if (raiseDialogState?.message !== "")
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
        onCloseHandler: onClickHandler,
        onButtonClickHandler: onClickHandler
    }}/>);
}
