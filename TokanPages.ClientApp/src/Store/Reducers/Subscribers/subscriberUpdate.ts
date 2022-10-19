import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { ISubscriberUpdate } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

import { 
    TKnownActions, 
    UPDATE_SUBSCRIBER, 
    UPDATE_SUBSCRIBER_RESPONSE, 
} from "../../Actions/Subscribers/subscriberUpdate";

export const SubscriberUpdate: 
    Reducer<ISubscriberUpdate> = (state: ISubscriberUpdate | undefined, incomingAction: Action): 
    ISubscriberUpdate => 
{
    if (state === undefined) return ApplicationDefault.subscriberUpdate;

    const action = incomingAction as TKnownActions;
    switch (action.type) 
    {
        case UPDATE_SUBSCRIBER:
            return { 
                status: OperationStatus.notStarted, 
                response: state.response 
            };

        case UPDATE_SUBSCRIBER_RESPONSE:
            return { 
                status: OperationStatus.hasFinished, 
                response: action.payload 
            };

        default: return state;
    }
};
