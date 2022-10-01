import { Action, Reducer } from "redux";
import { ApplicationDefaults } from "../../Configuration";
import { IUserActivate } from "../../States";
import { OperationStatus } from "../../../Shared/enums";
import { 
    TKnownActions,
    ACTIVATE_ACCOUNT,
    ACTIVATE_ACCOUNT_CLEAR,
    ACTIVATE_ACCOUNT_RESPONSE
} from "../../Actions/Users/activateAccountAction";

export const ActivateAccountReducer: 
    Reducer<IUserActivate> = (state: IUserActivate | undefined, incomingAction: Action): 
    IUserActivate => 
{
    if (state === undefined) return ApplicationDefaults.userActivate;

    const action = incomingAction as TKnownActions;
    switch (action.type) 
    {
        case ACTIVATE_ACCOUNT_CLEAR:
            return {
                operationStatus: OperationStatus.notStarted,
                attachedErrorObject: { }
            };
        case ACTIVATE_ACCOUNT:
            return { 
                operationStatus: OperationStatus.inProgress,
                attachedErrorObject: state.attachedErrorObject
            };

        case ACTIVATE_ACCOUNT_RESPONSE:
            return { 
                operationStatus: OperationStatus.hasFinished,
                attachedErrorObject: { }
            };

        default: return state;
    }
};
