import { Action, Reducer } from "redux";
import { ApplicationDefaults } from "../../Configuration";
import { ISigninUser } from "../../States";
import { OperationStatus } from "../../../Shared/enums";
import { 
    TKnownActions,
    SIGNIN_USER,
    SIGNIN_USER_CLEAR,
    SIGNIN_USER_RESPONSE
} from "../../Actions/Users/signinUserAction";

export const SigninUserReducer: 
    Reducer<ISigninUser> = (state: ISigninUser | undefined, incomingAction: Action): 
    ISigninUser => 
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
