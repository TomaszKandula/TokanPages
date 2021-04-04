import { Action, Reducer } from "redux";
import { IRaiseError } from "../States/raiseErrorState";
import { RaiseErrorDefault } from "../Defaults/raiseErrorDefault";
import { TKnownActions } from "../Actions/raiseErrorAction";

const RaiseErrorReducer: Reducer<IRaiseError> = (state: IRaiseError | undefined, incomingAction: Action): IRaiseError => 
{
    if (state === undefined) return RaiseErrorDefault;

    const action = incomingAction as TKnownActions;
    switch (action.type) 
    {
        //Add cases


        default: return state;
    }
};

export default RaiseErrorReducer;
