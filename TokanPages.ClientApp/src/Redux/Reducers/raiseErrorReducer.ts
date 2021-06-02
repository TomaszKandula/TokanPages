import { Action, Reducer } from "redux";
import { IRaiseError } from "../../Redux/States/raiseErrorState";
import { combinedDefaults } from "../../Redux/combinedDefaults";
import { CLEAR_ERROR, RAISE_ERROR, TErrorActions } from "../../Redux/Actions/raiseErrorAction";
import { DialogType } from "../../Shared/enums";
import { NO_ERRORS, RECEIVED_ERROR_MESSAGE } from "../../Shared/constants";

const RaiseErrorReducer: Reducer<IRaiseError> = (state: IRaiseError | undefined, incomingAction: Action): IRaiseError =>
{
    if (state === undefined) return combinedDefaults.raiseError;

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

export default RaiseErrorReducer;
