import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { IUserSignup } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

import { 
    TKnownActions,
    SIGNUP_USER,
    SIGNUP_USER_CLEAR,
    SIGNUP_USER_RESPONSE
} from "../../Actions/Users/userSignup";

export const UserSignup: Reducer<IUserSignup> = (state: IUserSignup | undefined, incomingAction: Action): IUserSignup => 
{
    if (state === undefined) return ApplicationDefault.userSignup;

    const action = incomingAction as TKnownActions;
    switch (action.type) 
    {
        case SIGNUP_USER_CLEAR:
            return {
                status: OperationStatus.notStarted,
                response: { }
            };
        case SIGNUP_USER:
            return { 
                status: OperationStatus.inProgress,
                response: state.response
            };

        case SIGNUP_USER_RESPONSE:
            return { 
                status: OperationStatus.hasFinished,
                response: action.payload
            };

        default: return state;
    }
};
