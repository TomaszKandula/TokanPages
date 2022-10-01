import { Action, Reducer } from "redux";
import { IRaiseDialog } from "../States";
import { ApplicationDefaults } from "../Configuration";
import { CLEAR_DIALOG, RAISE_DIALOG, TDialogActions } from "../Actions/raiseDialogAction";
import { IconType } from "../../Shared/enums";

export const RaiseDialogReducer: 
    Reducer<IRaiseDialog> = (state: IRaiseDialog | undefined, incomingAction: Action): 
    IRaiseDialog =>
{
    if (state === undefined) return ApplicationDefaults.applicationDialog;

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
