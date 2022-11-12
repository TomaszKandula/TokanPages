import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { IUserPasswordReset } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

import { 
    TKnownActions,
    RESET,
    CLEAR,
    RESPONSE
} from "../../Actions/Users/userPasswordReset";

export const UserPasswordReset: 
    Reducer<IUserPasswordReset> = (state: IUserPasswordReset | undefined, incomingAction: Action): 
    IUserPasswordReset => 
{
    if (state === undefined) return ApplicationDefault.userPasswordReset;

    const action = incomingAction as TKnownActions;
    switch (action.type) 
    {
        case CLEAR:
            return {
                status: OperationStatus.notStarted,
                response: { }
            };
        case RESET:
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
