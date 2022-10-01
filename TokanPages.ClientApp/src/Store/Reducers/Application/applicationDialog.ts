import { Action, Reducer } from "redux";
import { IApplicationDialog } from "../../States";
import { ApplicationDefault } from "../../Configuration";
import { CLEAR_DIALOG, RAISE_DIALOG, TDialogActions } from "../../Actions/Application/applicationDialog";
import { IconType } from "../../../Shared/enums";

export const ApplicationDialog: 
    Reducer<IApplicationDialog> = (state: IApplicationDialog | undefined, incomingAction: Action): 
    IApplicationDialog =>
{
    if (state === undefined) return ApplicationDefault.applicationDialog;

    const action = incomingAction as TDialogActions;
    switch(action.type)
    {
        case CLEAR_DIALOG:
            return {
                title: "",
                message: "",
                icon: IconType.info 
            }
        case RAISE_DIALOG:
            return { 
                title: action.dialog.title,
                message: action.dialog.message,
                icon: action.dialog.icon
            }
        default: return state;
    }
}
