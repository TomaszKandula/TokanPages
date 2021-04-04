import { Action, Reducer } from "redux";
import { combinedDefaults } from "../combinedDefaults";
import { IRemoveSubscriber } from "../../Redux/States/removeSubscriberState";
import { TKnownActions, API_REMOVE_SUBSCRIBER, API_REMOVE_SUBSCRIBER_RESPONSE } from "../Actions/removeSubscriberAction";

const RemoveSubscriberReducer: Reducer<IRemoveSubscriber> = (state: IRemoveSubscriber | undefined, incomingAction: Action): IRemoveSubscriber => 
{
    if (state === undefined) return combinedDefaults.removeSubscriber;

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
