import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { IUserReAuthenticate } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

import { 
    TKnownActions,
    REAUTHENTICATE_USER,
    REAUTHENTICATE_USER_CLEAR,
    REAUTHENTICATE_USER_RESPONSE
} from "../../Actions/Users/userReAuthenticate";

export const UserReAuthenticate: 
    Reducer<IUserReAuthenticate> = (state: IUserReAuthenticate | undefined, incomingAction: Action): 
    IUserReAuthenticate => 
{
    if (state === undefined) return ApplicationDefault.userReAuthenticate;

    const action = incomingAction as TKnownActions;
    switch (action.type) 
    {
        case REAUTHENTICATE_USER_CLEAR:
            return {
                status: OperationStatus.notStarted,
                response: { }
            };
        case REAUTHENTICATE_USER:
            return { 
                status: OperationStatus.inProgress,
                response: state.response
            };

        case REAUTHENTICATE_USER_RESPONSE:
            return { 
                status: OperationStatus.hasFinished,
                response: action.payload
            };

        default: return state;
    }
};
