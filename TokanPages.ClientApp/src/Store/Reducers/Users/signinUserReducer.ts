import { Action, Reducer } from "redux";
import { ApplicationDefaults } from "../../Configuration";
import { IUserSignin } from "../../States";
import { OperationStatus } from "../../../Shared/enums";
import { 
    TKnownActions,
    SIGNIN_USER,
    SIGNIN_USER_CLEAR,
    SIGNIN_USER_RESPONSE
} from "../../Actions/Users/signinUserAction";

export const UserSignin: 
    Reducer<IUserSignin> = (state: IUserSignin | undefined, incomingAction: Action): 
    IUserSignin => 
{
    if (state === undefined) return ApplicationDefaults.userSignin;

    const action = incomingAction as TKnownActions;
    switch (action.type) 
    {
        case SIGNIN_USER_CLEAR:
            return {
                operationStatus: OperationStatus.notStarted,
                attachedErrorObject: { }
            };
        case SIGNIN_USER:
            return { 
                operationStatus: OperationStatus.inProgress,
                attachedErrorObject: state.attachedErrorObject
            };

        case SIGNIN_USER_RESPONSE:
            return { 
                operationStatus: OperationStatus.hasFinished,
                attachedErrorObject: { }
            };

        default: return state;
    }
};
