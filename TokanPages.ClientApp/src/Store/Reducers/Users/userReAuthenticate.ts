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
