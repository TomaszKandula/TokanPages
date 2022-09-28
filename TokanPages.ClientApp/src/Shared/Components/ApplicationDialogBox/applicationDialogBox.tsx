import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { IApplicationState } from "../../../Store/Configuration";
import { DialogAction } from "../../../Store/Actions";
import { IRaiseDialog } from "../../../Store/States";
import { IconType } from "../../enums";
import { ApplicationDialogBoxView } from "./View/applicationDialogBoxView";
import Validate from "validate.js";

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

export const ApplicationDialogBox = (): JSX.Element => 
{
    const dispatch = useDispatch();
    const [dialogState, setDialogState] = React.useState(DialogState);
    const raiseDialogState = useSelector((state: IApplicationState) => state.raiseDialog);
    
    const clearDialog = React.useCallback(() => 
    { 
        if (!dialogState.state && !Validate.isEmpty(dialogState.message))
        {
            dispatch(DialogAction.clearDialog());
            setDialogState(DialogState);
        }
    }, 
    [ dispatch, dialogState ]);
    
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
