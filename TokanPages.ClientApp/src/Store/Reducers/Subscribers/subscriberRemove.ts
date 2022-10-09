import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { ISubscriberRemove } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

import { 
    TKnownActions, 
    REMOVE_SUBSCRIBER, 
    REMOVE_SUBSCRIBER_RESPONSE 
} from "../../Actions/Subscribers/subscriberRemove";

export const SubscriberRemove: 
    Reducer<ISubscriberRemove> = (state: ISubscriberRemove | undefined, incomingAction: Action): 
    ISubscriberRemove => 
{
    if (state === undefined) return ApplicationDefault.subscriberRemove;

    const action = incomingAction as TKnownActions;
    switch (action.type) 
    {
        case REMOVE_SUBSCRIBER:
            return { 
                operationStatus: OperationStatus.inProgress, 
                attachedErrorObject: state.attachedErrorObject 
            };

        case REMOVE_SUBSCRIBER_RESPONSE:
            return { 
                operationStatus: OperationStatus.hasFinished, 
                attachedErrorObject: { } 
            };

        default: return state;
    }
};
