import { Action, Reducer } from "redux";
import { ApplicationDefaults } from "../../Configuration";
import { IUpdateSubscriber } from "../../States";
import { OperationStatus } from "../../../Shared/enums";
import { 
    TKnownActions, 
    UPDATE_SUBSCRIBER, 
    UPDATE_SUBSCRIBER_RESPONSE, 
} from "../../Actions/Subscribers/updateSubscriberAction";

export const UpdateSubscriberReducer: 
    Reducer<IUpdateSubscriber> = (state: IUpdateSubscriber | undefined, incomingAction: Action): 
    IUpdateSubscriber => 
{
    if (state === undefined) return ApplicationDefaults.subscriberUpdate;

    const action = incomingAction as TKnownActions;
    switch (action.type) 
    {
        case UPDATE_SUBSCRIBER:
            return { 
                operationStatus: OperationStatus.notStarted, 
                attachedErrorObject: state.attachedErrorObject 
            };

        case UPDATE_SUBSCRIBER_RESPONSE:
            return { 
                operationStatus: OperationStatus.hasFinished, 
                attachedErrorObject: { } 
            };

        default: return state;
    }
};