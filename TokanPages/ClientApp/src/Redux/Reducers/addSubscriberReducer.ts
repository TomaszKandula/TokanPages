import { Action, Reducer } from "redux";
import { combinedDefaults } from "../../Redux/Defaults/combinedDefaults";
import { AddSubscriberEnum } from "../../Redux/Enums/addSubscriberEnum";
import { IAddSubscriber } from "../../Redux/States/addSubscriberState";
import { TKnownActions, API_ADD_SUBSCRIBER, API_ADD_SUBSCRIBER_RESPONSE, API_ADD_SUBSCRIBER_CLEAR } from "../Actions/addSubscriberAction";

const AddSubscriberReducer: Reducer<IAddSubscriber> = (state: IAddSubscriber | undefined, incomingAction: Action): IAddSubscriber => 
{
    if (state === undefined) return combinedDefaults.addSubscriber;

    const action = incomingAction as TKnownActions;
    switch (action.type) 
    {
        case API_ADD_SUBSCRIBER_CLEAR:
            return combinedDefaults.addSubscriber;

        case API_ADD_SUBSCRIBER:
            return { isAddingSubscriber: AddSubscriberEnum.inProgress, hasAddedSubscriber: state.hasAddedSubscriber };

        case API_ADD_SUBSCRIBER_RESPONSE:
            return { isAddingSubscriber: AddSubscriberEnum.hasFinished, hasAddedSubscriber: action.hasAddedSubscriber };

        default: return state;
    }
};

export default AddSubscriberReducer;
