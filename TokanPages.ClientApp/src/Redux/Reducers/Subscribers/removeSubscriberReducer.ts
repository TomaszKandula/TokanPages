import { Action, Reducer } from "redux";
import { combinedDefaults } from "../../combinedDefaults";
import { IRemoveSubscriber } from "../../../Redux/States/Subscribers/removeSubscriberState";
import { OperationStatus } from "../../../Shared/enums";
import { 
    TKnownActions, 
    REMOVE_SUBSCRIBER, 
    REMOVE_SUBSCRIBER_RESPONSE 
} from "../../Actions/Subscribers/removeSubscriberAction";

export const RemoveSubscriberReducer: Reducer<IRemoveSubscriber> = (state: IRemoveSubscriber | undefined, incomingAction: Action): IRemoveSubscriber => 
{
    if (state === undefined) return combinedDefaults.removeSubscriber;

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
