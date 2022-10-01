import { Action, Reducer } from "redux";
import { IRaiseError } from "../States";
import { ApplicationDefaults } from "../Configuration";
import { CLEAR_ERROR, RAISE_ERROR, TErrorActions } from "../Actions/raiseErrorAction";
import { DialogType } from "../../Shared/enums";
import { NO_ERRORS, RECEIVED_ERROR_MESSAGE } from "../../Shared/constants";

export const RaiseErrorReducer: 
    Reducer<IRaiseError> = (state: IRaiseError | undefined, incomingAction: Action): 
    IRaiseError =>
{
    if (state === undefined) return ApplicationDefaults.applicationError;

    const action = incomingAction as TErrorActions;
    switch(action.type)
    {
        case CLEAR_ERROR:
            return {
                defaultErrorMessage: NO_ERRORS,
                attachedErrorObject: { },
                dialogType: DialogType.toast
            }
        case RAISE_ERROR:
            return { 
                defaultErrorMessage: RECEIVED_ERROR_MESSAGE, 
                attachedErrorObject: action.errorObject,
                dialogType: action.dialogType ?? DialogType.toast
            }
        default: return state;
    }
}