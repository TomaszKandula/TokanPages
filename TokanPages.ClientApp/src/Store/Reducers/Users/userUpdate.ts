import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { UserUpdateState } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

import { 
    TKnownActions,
    UPDATE,
    CLEAR,
    RESPONSE
} from "../../Actions/Users/userUpdate";

export const UserUpdate: 
    Reducer<UserUpdateState> = (state: UserUpdateState | undefined, incomingAction: Action): 
    UserUpdateState => 
{
    if (state === undefined) return ApplicationDefault.userUpdate;

    const action = incomingAction as TKnownActions;
    switch (action.type) 
    {
        case CLEAR:
            return {
                status: OperationStatus.notStarted,
                response: { }
            };
        case UPDATE:
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
