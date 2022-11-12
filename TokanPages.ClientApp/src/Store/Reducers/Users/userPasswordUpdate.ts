import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { IUserPasswordUpdate } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

import { 
    TKnownActions,
    UPDATE,
    CLEAR,
    RESPONSE
} from "../../Actions/Users/userPasswordUpdate";

export const UserPasswordUpdate: 
    Reducer<IUserPasswordUpdate> = (state: IUserPasswordUpdate | undefined, incomingAction: Action): 
    IUserPasswordUpdate => 
{
    if (state === undefined) return ApplicationDefault.userPasswordUpdate;

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
