import { Action, Reducer } from "redux";
import { ApplicationDefaults } from "../../Configuration";
import { IUpdateUserPassword } from "../../States";
import { OperationStatus } from "../../../Shared/enums";
import { 
    TKnownActions,
    UPDATE_USER_PASSWORD,
    UPDATE_USER_PASSWORD_CLEAR,
    UPDATE_USER_PASSWORD_RESPONSE
} from "../../Actions/Users/updateUserPasswordAction";

export const UpdateUserPasswordReducer: 
    Reducer<IUpdateUserPassword> = (state: IUpdateUserPassword | undefined, incomingAction: Action): 
    IUpdateUserPassword => 
{
    if (state === undefined) return ApplicationDefaults.updateUserPassword;

    const action = incomingAction as TKnownActions;
    switch (action.type) 
    {
        case UPDATE_USER_PASSWORD_CLEAR:
            return {
                operationStatus: OperationStatus.notStarted,
                attachedErrorObject: { }
            };
        case UPDATE_USER_PASSWORD:
            return { 
                operationStatus: OperationStatus.inProgress,
                attachedErrorObject: state.attachedErrorObject
            };

        case UPDATE_USER_PASSWORD_RESPONSE:
            return { 
                operationStatus: OperationStatus.hasFinished,
                attachedErrorObject: { }
            };

        default: return state;
    }
};
