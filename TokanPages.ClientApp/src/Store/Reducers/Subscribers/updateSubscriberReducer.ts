import { Action, Reducer } from "redux";
import { combinedDefaults } from "../../combinedDefaults";
import { IUpdateSubscriber } from "../../States/Subscribers/updateSubscriberState";
import { OperationStatus } from "../../../Shared/enums";
import { 
    TKnownActions, 
    UPDATE_SUBSCRIBER, 
    UPDATE_SUBSCRIBER_RESPONSE, 
} from "../../Actions/Subscribers/updateSubscriberAction";

export const UpdateSubscriberReducer: Reducer<IUpdateSubscriber> = (state: IUpdateSubscriber | undefined, incomingAction: Action): IUpdateSubscriber => 
{
    if (state === undefined) return combinedDefaults.updateSubscriber;

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
