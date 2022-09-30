import { Action, Reducer } from "redux";
import { ApplicationDefaults } from "../../Configuration";
import { IUpdateUser } from "../../States";
import { OperationStatus } from "../../../Shared/enums";
import { 
    TKnownActions,
    UPDATE_USER,
    UPDATE_USER_CLEAR,
    UPDATE_USER_RESPONSE
} from "../../Actions/Users/updateUserAction";

export const UpdateUserReducer: 
    Reducer<IUpdateUser> = (state: IUpdateUser | undefined, incomingAction: Action): 
    IUpdateUser => 
{
    if (state === undefined) return ApplicationDefaults.updateUser;

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
