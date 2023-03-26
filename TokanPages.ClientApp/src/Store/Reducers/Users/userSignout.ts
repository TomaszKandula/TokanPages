import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { UserSignoutState } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

import { 
    TKnownActions,
    SIGNOUT,
    CLEAR,
    RESPONSE
} from "../../Actions/Users/userSignout";

export const UserSignout: 
    Reducer<UserSignoutState> = (state: UserSignoutState | undefined, incomingAction: Action): 
    UserSignoutState => 
{
    if (state === undefined) return ApplicationDefault.userSignin;

    const action = incomingAction as TKnownActions;
    switch (action.type) 
    {
        case CLEAR:
            return {
                status: OperationStatus.notStarted
            };

        case SIGNOUT:
            return { 
                status: OperationStatus.inProgress
            };

        case RESPONSE:
            return { 
                status: OperationStatus.hasFinished
            };

        default: return state;
    }
};
