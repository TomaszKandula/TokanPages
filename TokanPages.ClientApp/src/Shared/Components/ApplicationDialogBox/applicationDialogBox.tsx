import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { IApplicationState } from "../../../Store/Configuration";
import { ApplicationDialog } from "../../../Store/Actions";
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
    const dialog = useSelector((state: IApplicationState) => state.applicationDialog);
    
    const clearDialog = React.useCallback(() => 
    { 
        if (!dialogState.state && !Validate.isEmpty(dialogState.message))
        {
            dispatch(ApplicationDialog.clear());
            setDialogState(DialogState);
        }
    }, 
    [ dispatch, dialogState ]);
    
    const raiseDialog = React.useCallback(() => 
    {
        if (!Validate.isEmpty(dialog?.message))
        {
            setDialogState(
            { 
                state: true,
                title: dialog?.title,
                message: dialog?.message,
                icon: dialog?.icon
            });
        }
    }, 
    [ dialog ]);

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
