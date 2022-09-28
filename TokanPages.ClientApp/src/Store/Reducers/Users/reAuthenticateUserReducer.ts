import { Action, Reducer } from "redux";
import { CombinedDefaults } from "../../Configuration";
import { IReAuthenticateUser } from "../../States/Users/reAuthenticateUserState";
import { OperationStatus } from "../../../Shared/enums";
import { 
    TKnownActions,
    REAUTHENTICATE_USER,
    REAUTHENTICATE_USER_CLEAR,
    REAUTHENTICATE_USER_RESPONSE
} from "../../Actions/Users/reAuthenticateUserAction";

export const ReAuthenticateUserReducer: Reducer<IReAuthenticateUser> = (state: IReAuthenticateUser | undefined, incomingAction: Action): IReAuthenticateUser => 
{
    if (state === undefined) return CombinedDefaults.signinUser;

    const action = incomingAction as TKnownActions;
    switch (action.type) 
    {
        case REAUTHENTICATE_USER_CLEAR:
            return {
                operationStatus: OperationStatus.notStarted,
                attachedErrorObject: { }
            };
        case REAUTHENTICATE_USER:
            return { 
                operationStatus: OperationStatus.inProgress,
                attachedErrorObject: state.attachedErrorObject
            };

        case REAUTHENTICATE_USER_RESPONSE:
            return { 
                operationStatus: OperationStatus.hasFinished,
                attachedErrorObject: { }
            };

        default: return state;
    }
};
