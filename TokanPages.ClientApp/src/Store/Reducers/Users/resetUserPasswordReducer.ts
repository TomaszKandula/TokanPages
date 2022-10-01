import { Action, Reducer } from "redux";
import { ApplicationDefaults } from "../../Configuration";
import { IResetUserPassword } from "../../States";
import { OperationStatus } from "../../../Shared/enums";
import { 
    TKnownActions,
    RESET_USER_PASSWORD,
    RESET_USER_PASSWORD_CLEAR,
    RESET_USER_PASSWORD_RESPONSE
} from "../../Actions/Users/resetUserPasswordAction";

export const ResetUserPasswordReducer: 
    Reducer<IResetUserPassword> = (state: IResetUserPassword | undefined, incomingAction: Action): 
    IResetUserPassword => 
{
    if (state === undefined) return ApplicationDefaults.userPasswordReset;

    const action = incomingAction as TKnownActions;
    switch (action.type) 
    {
        case RESET_USER_PASSWORD_CLEAR:
            return {
                operationStatus: OperationStatus.notStarted,
                attachedErrorObject: { }
            };
        case RESET_USER_PASSWORD:
            return { 
                operationStatus: OperationStatus.inProgress,
                attachedErrorObject: state.attachedErrorObject
            };

        case RESET_USER_PASSWORD_RESPONSE:
            return { 
                operationStatus: OperationStatus.hasFinished,
                attachedErrorObject: { }
            };

        default: return state;
    }
};
