import { Action, Reducer } from "redux";
import { TKnownActions, API_UPDATE_SUBSCRIBER, API_UPDATE_SUBSCRIBER_RESPONSE } from "../Actions/updateSubscriberAction";
import { UpdateSubscriberDefaultValues, IUpdateSubscriber,  } from "../applicationState";

const UpdateSubscriberReducer: Reducer<IUpdateSubscriber> = (state: IUpdateSubscriber | undefined, incomingAction: Action): IUpdateSubscriber => 
{
    if (state === undefined) return UpdateSubscriberDefaultValues;

    const action = incomingAction as TKnownActions;
    switch (action.type) 
    {
        case API_UPDATE_SUBSCRIBER:
            return { isUpdatingSubscriber: true, hasUpdatedSubscriber: state.hasUpdatedSubscriber };

        case API_UPDATE_SUBSCRIBER_RESPONSE:
            return { isUpdatingSubscriber: false, hasUpdatedSubscriber: action.hasUpdatedSubscriber };

        default: return state;
    }
};

export default UpdateSubscriberReducer;
