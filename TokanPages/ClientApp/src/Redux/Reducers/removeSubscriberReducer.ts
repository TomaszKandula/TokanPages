import { Action, Reducer } from "redux";
import { combinedDefaults } from "../combinedDefaults";
import { IRemoveSubscriber } from "../../Redux/States/removeSubscriberState";
import { 
    TKnownActions, 
    REMOVE_SUBSCRIBER, 
    REMOVE_SUBSCRIBER_RESPONSE, 
    REMOVE_SUBSCRIBER_ERROR 
} from "../Actions/removeSubscriberAction";

const RemoveSubscriberReducer: Reducer<IRemoveSubscriber> = (state: IRemoveSubscriber | undefined, incomingAction: Action): IRemoveSubscriber => 
{
    if (state === undefined) return combinedDefaults.removeSubscriber;

    const action = incomingAction as TKnownActions;
    switch (action.type) 
    {
        case REMOVE_SUBSCRIBER:
            return { 
                isRemovingSubscriber: true, 
                hasRemovedSubscriber: state.hasRemovedSubscriber, 
                attachedErrorObject: state.attachedErrorObject 
            };

        case REMOVE_SUBSCRIBER_RESPONSE:
            return { 
                isRemovingSubscriber: false, 
                hasRemovedSubscriber: action.hasRemovedSubscriber, 
                attachedErrorObject: { } 
            };

        case REMOVE_SUBSCRIBER_ERROR:
            return { 
                isRemovingSubscriber: false, 
                hasRemovedSubscriber: false, 
                attachedErrorObject: action.errorObject 
            };

        default: return state;
    }
};

export default RemoveSubscriberReducer;
