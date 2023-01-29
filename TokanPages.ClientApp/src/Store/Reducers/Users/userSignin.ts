import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { UserSigninState } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

import { 
    TKnownActions,
    SIGNIN,
    CLEAR,
    RESPONSE
} from "../../Actions/Users/userSignin";

export const UserSignin: 
    Reducer<UserSigninState> = (state: UserSigninState | undefined, incomingAction: Action): 
    UserSigninState => 
{
    if (state === undefined) return ApplicationDefault.userSignin;

    const action = incomingAction as TKnownActions;
    switch (action.type) 
    {
        case CLEAR:
            return {
                status: OperationStatus.notStarted,
                response: { }
            };

        case SIGNIN:
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
