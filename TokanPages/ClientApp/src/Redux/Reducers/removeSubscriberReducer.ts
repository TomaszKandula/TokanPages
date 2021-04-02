import { Action, Reducer } from "redux";
import { TKnownActions, API_REMOVE_SUBSCRIBER, API_REMOVE_SUBSCRIBER_RESPONSE } from "../Actions/removeSubscriberAction";
import { RemoveSubscriberDefaultValues, IRemoveSubscriber,  } from "../applicationState";

const RemoveSubscriberReducer: Reducer<IRemoveSubscriber> = (state: IRemoveSubscriber | undefined, incomingAction: Action): IRemoveSubscriber => 
{
    if (state === undefined) return RemoveSubscriberDefaultValues;

    const action = incomingAction as TKnownActions;
    switch (action.type) 
    {
        case API_REMOVE_SUBSCRIBER:
            return { isRemovingSubscriber: true, hasRemovedSubscriber: state.hasRemovedSubscriber };

        case API_REMOVE_SUBSCRIBER_RESPONSE:
            return { isRemovingSubscriber: false, hasRemovedSubscriber: action.hasRemovedSubscriber };

        default: return state;
    }
};

export default RemoveSubscriberReducer;
