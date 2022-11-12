import { Action, Reducer } from "redux";
import { IApplicationError } from "../../States";
import { ApplicationDefault } from "../../Configuration";
import { DialogType } from "../../../Shared/enums";

import { 
    CLEAR, 
    RAISE, 
    TErrorActions 
} from "../../Actions/Application/applicationError";

import { 
    NO_ERRORS, 
    RECEIVED_ERROR_MESSAGE 
} from "../../../Shared/constants";

export const ApplicationError: 
    Reducer<IApplicationError> = (state: IApplicationError | undefined, incomingAction: Action): 
    IApplicationError =>
{
    if (state === undefined) return ApplicationDefault.applicationError;

    const action = incomingAction as TErrorActions;
    switch(action.type)
    {
        case CLEAR:
            return {
                errorMessage: NO_ERRORS,
                errorDetails: { },
                dialogType: DialogType.toast
            }
        case RAISE:
            return { 
                errorMessage: RECEIVED_ERROR_MESSAGE, 
                errorDetails: action.errorDetails,
                dialogType: action.dialogType ?? DialogType.toast
            }
        default: return state;
    }
}
