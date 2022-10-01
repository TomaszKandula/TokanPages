import { Action, Reducer } from "redux";
import { ApplicationDefaults } from "../../Configuration";
import { IUserUpdate } from "../../States";
import { OperationStatus } from "../../../Shared/enums";
import { 
    TKnownActions,
    UPDATE_USER,
    UPDATE_USER_CLEAR,
    UPDATE_USER_RESPONSE
} from "../../Actions/Users/updateUserAction";

export const UpdateUserReducer: 
    Reducer<IUserUpdate> = (state: IUserUpdate | undefined, incomingAction: Action): 
    IUserUpdate => 
{
    if (state === undefined) return ApplicationDefaults.userUpdate;

    const action = incomingAction as TKnownActions;
    switch (action.type) 
    {
        case UPDATE_USER_CLEAR:
            return {
                operationStatus: OperationStatus.notStarted,
                attachedErrorObject: { }
            };
        case UPDATE_USER:
            return { 
                operationStatus: OperationStatus.inProgress,
                attachedErrorObject: state.attachedErrorObject
            };

        case UPDATE_USER_RESPONSE:
            return { 
                operationStatus: OperationStatus.hasFinished,
                attachedErrorObject: { }
            };

        default: return state;
    }
};
