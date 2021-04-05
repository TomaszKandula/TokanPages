import { Action, Reducer } from "redux";
import { combinedDefaults } from "../combinedDefaults";
import { IUpdateSubscriber } from "../../Redux/States/updateSubscriberState";
import { 
    TKnownActions, 
    UPDATE_SUBSCRIBER, 
    UPDATE_SUBSCRIBER_RESPONSE, 
    UPDATE_SUBSCRIBER_ERROR 
} from "../Actions/updateSubscriberAction";

const UpdateSubscriberReducer: Reducer<IUpdateSubscriber> = (state: IUpdateSubscriber | undefined, incomingAction: Action): IUpdateSubscriber => 
{
    if (state === undefined) return combinedDefaults.updateSubscriber;

    const action = incomingAction as TKnownActions;
    switch (action.type) 
    {
        case UPDATE_SUBSCRIBER:
            return { 
                isUpdatingSubscriber: true, 
                hasUpdatedSubscriber: state.hasUpdatedSubscriber, 
                attachedErrorObject: state.attachedErrorObject 
            };

        case UPDATE_SUBSCRIBER_RESPONSE:
            return { 
                isUpdatingSubscriber: false, 
                hasUpdatedSubscriber: action.hasUpdatedSubscriber, 
                attachedErrorObject: { } 
            };

        case UPDATE_SUBSCRIBER_ERROR:
            return { 
                isUpdatingSubscriber: false, 
                hasUpdatedSubscriber: false, 
                attachedErrorObject: action.errorObject 
            };

        default: return state;
    }
};

export default UpdateSubscriberReducer;
