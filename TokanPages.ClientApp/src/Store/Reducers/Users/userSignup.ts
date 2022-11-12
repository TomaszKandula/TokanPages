import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { IUserSignup } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

import { 
    TKnownActions,
    SIGNUP,
    CLEAR,
    RESPONSE
} from "../../Actions/Users/userSignup";

export const UserSignup: Reducer<IUserSignup> = (state: IUserSignup | undefined, incomingAction: Action): IUserSignup => 
{
    if (state === undefined) return ApplicationDefault.userSignup;

    const action = incomingAction as TKnownActions;
    switch (action.type) 
    {
        case CLEAR:
            return {
                status: OperationStatus.notStarted,
                response: { }
            };
        case SIGNUP:
            return { 
                status: OperationStatus.inProgress,
                response: state.response
            };

        case RESPONSE:
            return { 
                status: OperationStatus.hasFinished,
                response: action.payload
            };

        default: return state;
    }
};
