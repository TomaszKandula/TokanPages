import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { IUserActivate } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

import { 
    TKnownActions,
    ACTIVATE,
    CLEAR,
    RESPONSE
} from "../../Actions/Users/userActivate";

export const UserActivate: 
    Reducer<IUserActivate> = (state: IUserActivate | undefined, incomingAction: Action): 
    IUserActivate => 
{
    if (state === undefined) return ApplicationDefault.userActivate;

    const action = incomingAction as TKnownActions;
    switch (action.type) 
    {
        case CLEAR:
            return {
                status: OperationStatus.notStarted,
                response: { }
            };
        case ACTIVATE:
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
