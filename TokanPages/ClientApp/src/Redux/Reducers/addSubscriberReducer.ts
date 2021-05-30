import { Action, Reducer } from "redux";
import { combinedDefaults } from "../combinedDefaults";
import { IAddSubscriber } from "../../Redux/States/addSubscriberState";
import { OperationStatus } from "../../Shared/enums";
import { 
    TKnownActions, 
    ADD_SUBSCRIBER, 
    ADD_SUBSCRIBER_RESPONSE, 
    ADD_SUBSCRIBER_CLEAR, 
    ADD_SUBSCRIBER_ERROR 
} from "../Actions/addSubscriberAction";

const AddSubscriberReducer: Reducer<IAddSubscriber> = (state: IAddSubscriber | undefined, incomingAction: Action): IAddSubscriber => 
{
    if (state === undefined) return combinedDefaults.addSubscriber;

    const action = incomingAction as TKnownActions;
    switch (action.type) 
    {
        case ADD_SUBSCRIBER_CLEAR:
            return combinedDefaults.addSubscriber;

        case ADD_SUBSCRIBER:
            return { 
                operationStatus: OperationStatus.inProgress, 
                attachedErrorObject: state.attachedErrorObject
            };

        case ADD_SUBSCRIBER_RESPONSE:
            return { 
                operationStatus: OperationStatus.hasFinished, 
                attachedErrorObject: { }
            };

        case ADD_SUBSCRIBER_ERROR:
            return { 
                operationStatus: OperationStatus.hasFailed, 
                attachedErrorObject: action.errorObject 
            };

        default: return state;
    }
};

export default AddSubscriberReducer;
