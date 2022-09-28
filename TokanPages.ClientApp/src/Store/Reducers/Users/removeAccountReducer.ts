import { Action, Reducer } from "redux";
import { combinedDefaults } from "../../combinedDefaults";
import { IRemoveAccount } from "../../States/Users/removeAccountState";
import { OperationStatus } from "../../../Shared/enums";
import { 
    TKnownActions,
    REMOVE_ACCOUNT,
    REMOVE_ACCOUNT_CLEAR,
    REMOVE_ACCOUNT_RESPONSE
} from "../../Actions/Users/removeAccountAction";

export const RemoveAccountReducer: Reducer<IRemoveAccount> = (state: IRemoveAccount | undefined, incomingAction: Action): IRemoveAccount => 
{
    if (state === undefined) return combinedDefaults.removeAccount;

    const action = incomingAction as TKnownActions;
    switch (action.type) 
    {
        case REMOVE_ACCOUNT_CLEAR:
            return {
                operationStatus: OperationStatus.notStarted,
                attachedErrorObject: { }
            };
        case REMOVE_ACCOUNT:
            return { 
                operationStatus: OperationStatus.inProgress,
                attachedErrorObject: state.attachedErrorObject
            };

        case REMOVE_ACCOUNT_RESPONSE:
            return { 
                operationStatus: OperationStatus.hasFinished,
                attachedErrorObject: { }
            };

        default: return state;
    }
};
