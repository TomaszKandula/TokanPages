import { Action, Reducer } from "redux";
import { combinedDefaults } from "../combinedDefaults";
import { IAddSubscriber } from "../../Redux/States/addSubscriberState";
import { OperationStatus } from "../../Shared/enums";
import { 
    TKnownActions, 
    API_ADD_SUBSCRIBER, 
    API_ADD_SUBSCRIBER_RESPONSE, 
    API_ADD_SUBSCRIBER_CLEAR, 
    ADD_SUBSCRIBER_ERROR 
} from "../Actions/addSubscriberAction";

const AddSubscriberReducer: Reducer<IAddSubscriber> = (state: IAddSubscriber | undefined, incomingAction: Action): IAddSubscriber => 
{
    if (state === undefined) return combinedDefaults.addSubscriber;

    const action = incomingAction as TKnownActions;
    switch (action.type) 
    {
        case API_ADD_SUBSCRIBER_CLEAR:
            return combinedDefaults.addSubscriber;

        case API_ADD_SUBSCRIBER:
            return { 
                isAddingSubscriber: OperationStatus.inProgress, 
                hasAddedSubscriber: state.hasAddedSubscriber, 
                attachedErrorObject: state.attachedErrorObject
            };

        case API_ADD_SUBSCRIBER_RESPONSE:
            return { 
                isAddingSubscriber: OperationStatus.hasFinished, 
                hasAddedSubscriber: action.hasAddedSubscriber, 
                attachedErrorObject: { }
            };

        case ADD_SUBSCRIBER_ERROR:
            return { 
                isAddingSubscriber: OperationStatus.hasFailed, 
                hasAddedSubscriber: false, 
                attachedErrorObject: action.errorObject 
            };

        default: return state;
    }
};

export default AddSubscriberReducer;
