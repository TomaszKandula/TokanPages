import { Action, Reducer } from "redux";
import { combinedDefaults } from "../../combinedDefaults";
import { IActivateAccount } from "../../../Redux/States/Users/activateAccountState";
import { OperationStatus } from "../../../Shared/enums";
import { 
    TKnownActions,
    ACTIVATE_ACCOUNT,
    ACTIVATE_ACCOUNT_CLEAR,
    ACTIVATE_ACCOUNT_RESPONSE
} from "../../Actions/Users/activateAccountAction";

const ActivateAccountReducer: Reducer<IActivateAccount> = (state: IActivateAccount | undefined, incomingAction: Action): IActivateAccount => 
{
    if (state === undefined) return combinedDefaults.activateAccount;

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

export default ActivateAccountReducer;
