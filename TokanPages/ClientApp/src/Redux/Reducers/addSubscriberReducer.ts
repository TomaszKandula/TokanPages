import { Action, Reducer } from "redux";
import { AddSubscriberDefaultValues } from "../../Redux/Defaults/addSubscriberDefault";
import { IAddSubscriber } from "../../Redux/States/addSubscriberState";
import { TKnownActions, API_ADD_SUBSCRIBER, API_ADD_SUBSCRIBER_RESPONSE } from "../Actions/addSubscriberAction";

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
