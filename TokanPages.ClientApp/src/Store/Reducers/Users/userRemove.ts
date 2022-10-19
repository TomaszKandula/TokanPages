import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { IUserRemove } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

import { 
    TKnownActions,
    REMOVE_ACCOUNT,
    REMOVE_ACCOUNT_CLEAR,
    REMOVE_ACCOUNT_RESPONSE
} from "../../Actions/Users/userRemove";

export const UserRemove: 
    Reducer<IUserRemove> = (state: IUserRemove | undefined, incomingAction: Action): 
    IUserRemove => 
{
    if (state === undefined) return ApplicationDefault.userRemove;

    const action = incomingAction as TKnownActions;
    switch (action.type) 
    {
        case REMOVE_ACCOUNT_CLEAR:
            return {
                status: OperationStatus.notStarted,
                response: { }
            };
        case REMOVE_ACCOUNT:
            return { 
                status: OperationStatus.inProgress,
                response: state.response
            };

        case REMOVE_ACCOUNT_RESPONSE:
            return { 
                status: OperationStatus.hasFinished,
                response: action.payload
            };

        default: return state;
    }
};
