import { Action, Reducer } from "redux";
import { TKnownActions, API_ADD_SUBSCRIBER, API_ADD_SUBSCRIBER_RESPONSE } from "../Actions/addSubscriberAction";
import { AddSubscriberDefaultValues, IAddSubscriber,  } from "../applicationState";

const AddSubscriberReducer: Reducer<IAddSubscriber> = (state: IAddSubscriber | undefined, incomingAction: Action): IAddSubscriber => 
{
    if (state === undefined) return AddSubscriberDefaultValues;

    const action = incomingAction as TKnownActions;
    switch (action.type) 
    {
        case API_ADD_SUBSCRIBER:
            return { isAddingSubscriber: true, hasAddedSubscriber: state.hasAddedSubscriber };

        case API_ADD_SUBSCRIBER_RESPONSE:
            return { isAddingSubscriber: false, hasAddedSubscriber: action.hasAddedSubscriber };

        default: return state;
    }
};

export default AddSubscriberReducer;
