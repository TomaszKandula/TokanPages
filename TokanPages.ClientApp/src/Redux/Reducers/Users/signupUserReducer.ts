import { Action, Reducer } from "redux";
import { combinedDefaults } from "../../combinedDefaults";
import { ISignupUser } from "../../States/Users/signupUserState";
import { OperationStatus } from "../../../Shared/enums";
import { 
    TKnownActions,
    SIGNUP_USER,
    SIGNUP_USER_CLEAR,
    SIGNUP_USER_RESPONSE
} from "../../Actions/Users/signupUserAction";

const SignupUserReducer: Reducer<ISignupUser> = (state: ISignupUser | undefined, incomingAction: Action): ISignupUser => 
{
    if (state === undefined) return combinedDefaults.signupUser;

    const action = incomingAction as TKnownActions;
    switch (action.type) 
    {
        case SIGNUP_USER_CLEAR:
            return {
                operationStatus: OperationStatus.notStarted,
                attachedErrorObject: { }
            };
        case SIGNUP_USER:
            return { 
                operationStatus: OperationStatus.inProgress,
                attachedErrorObject: state.attachedErrorObject
            };

        case SIGNUP_USER_RESPONSE:
            return { 
                operationStatus: OperationStatus.hasFinished,
                attachedErrorObject: { }
            };

        default: return state;
    }
};

export default SignupUserReducer;
