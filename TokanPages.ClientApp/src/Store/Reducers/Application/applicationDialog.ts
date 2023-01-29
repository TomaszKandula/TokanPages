import { Action, Reducer } from "redux";
import { ApplicationDialogState } from "../../States";
import { ApplicationDefault } from "../../Configuration";
import { IconType } from "../../../Shared/enums";

import { 
    CLEAR, 
    RAISE, 
    TDialogActions 
} from "../../Actions/Application/applicationDialog";

export const ApplicationDialog: 
    Reducer<ApplicationDialogState> = (state: ApplicationDialogState | undefined, incomingAction: Action): 
    ApplicationDialogState =>
{
    if (state === undefined) return ApplicationDefault.applicationDialog;

    const action = incomingAction as TDialogActions;
    switch(action.type)
    {
        case CLEAR:
            return {
                title: "",
                message: "",
                icon: IconType.info 
            }
        case RAISE:
            return { 
                title: action.dialog.title,
                message: action.dialog.message,
                icon: action.dialog.icon
            }
        default: return state;
    }
}
