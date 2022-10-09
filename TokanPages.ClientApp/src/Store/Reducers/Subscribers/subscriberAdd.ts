import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { ISubscriberAdd } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

import { 
    TKnownActions, 
    ADD_SUBSCRIBER, 
    ADD_SUBSCRIBER_RESPONSE, 
    ADD_SUBSCRIBER_CLEAR, 
} from "../../Actions/Subscribers/subscriberAdd";

export const SubscriberAdd: 
    Reducer<ISubscriberAdd> = (state: ISubscriberAdd | undefined, incomingAction: Action): 
    ISubscriberAdd => 
{
    if (state === undefined) return ApplicationDefault.subscriberAdd;

    const action = incomingAction as TKnownActions;
    switch (action.type) 
    {
        case ADD_SUBSCRIBER_CLEAR:
            return ApplicationDefault.subscriberAdd;

        case ADD_SUBSCRIBER:
            return { 
                operationStatus: OperationStatus.inProgress, 
                attachedErrorObject: state.attachedErrorObject
            };

        case ADD_SUBSCRIBER_RESPONSE:
            return { 
                operationStatus: OperationStatus.hasFinished, 
                attachedErrorObject: { }
            };

        default: return state;
    }
};
