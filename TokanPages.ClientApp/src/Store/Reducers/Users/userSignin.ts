import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { IUserSignin } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

import { 
    TKnownActions,
    SIGNIN_USER,
    SIGNIN_USER_CLEAR,
    SIGNIN_USER_RESPONSE
} from "../../Actions/Users/userSignin";

export const UserSignin: 
    Reducer<IUserSignin> = (state: IUserSignin | undefined, incomingAction: Action): 
    IUserSignin => 
{
    if (state === undefined) return ApplicationDefault.userSignin;

    const action = incomingAction as TKnownActions;
    switch (action.type) 
    {
        case SIGNIN_USER_CLEAR:
            return {
                status: OperationStatus.notStarted,
                response: { }
            };
        case SIGNIN_USER:
            return { 
                status: OperationStatus.inProgress,
                response: state.response
            };

        case SIGNIN_USER_RESPONSE:
            return { 
                status: OperationStatus.hasFinished,
                response: action.payload
            };

        default: return state;
    }
};
