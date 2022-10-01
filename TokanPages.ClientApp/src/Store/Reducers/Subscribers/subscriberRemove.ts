import { Action, Reducer } from "redux";
import { ApplicationDefaults } from "../../Configuration";
import { ISubscriberRemove } from "../../States";
import { OperationStatus } from "../../../Shared/enums";
import { 
    TKnownActions, 
    REMOVE_SUBSCRIBER, 
    REMOVE_SUBSCRIBER_RESPONSE 
} from "../../Actions/Subscribers/removeSubscriberAction";

export const SubscriberRemove: 
    Reducer<ISubscriberRemove> = (state: ISubscriberRemove | undefined, incomingAction: Action): 
    ISubscriberRemove => 
{
    if (state === undefined) return ApplicationDefaults.subscriberRemove;

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
