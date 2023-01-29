import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { SubscriberRemoveState } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

import { 
    TKnownActions, 
    REMOVE, 
    RESPONSE 
} from "../../Actions/Subscribers/subscriberRemove";

export const SubscriberRemove: 
    Reducer<SubscriberRemoveState> = (state: SubscriberRemoveState | undefined, incomingAction: Action): 
    SubscriberRemoveState => 
{
    if (state === undefined) return ApplicationDefault.subscriberRemove;

    const action = incomingAction as TKnownActions;
    switch (action.type) 
    {
        case REMOVE:
            return { 
                status: OperationStatus.inProgress, 
                response: state.response 
            };

        case RESPONSE:
            return { 
                status: OperationStatus.hasFinished, 
                response: action.payload
            };

        default: return state;
    }
};
