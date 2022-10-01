import { Action, Reducer } from "redux";
import { ApplicationDefaults } from "../../Configuration";
import { IUserSignup } from "../../States/Users/signupUserState";
import { OperationStatus } from "../../../Shared/enums";
import { 
    TKnownActions,
    SIGNUP_USER,
    SIGNUP_USER_CLEAR,
    SIGNUP_USER_RESPONSE
} from "../../Actions/Users/signupUserAction";

export const SignupUserReducer: Reducer<IUserSignup> = (state: IUserSignup | undefined, incomingAction: Action): IUserSignup => 
{
    if (state === undefined) return ApplicationDefaults.userSignup;

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
