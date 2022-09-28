import { Action, Reducer } from "redux";
import { CombinedDefaults } from "../../Configuration";
import { IAddSubscriber } from "../../States/Subscribers/addSubscriberState";
import { OperationStatus } from "../../../Shared/enums";
import { 
    TKnownActions, 
    ADD_SUBSCRIBER, 
    ADD_SUBSCRIBER_RESPONSE, 
    ADD_SUBSCRIBER_CLEAR, 
} from "../../Actions/Subscribers/addSubscriberAction";

export const AddSubscriberReducer: Reducer<IAddSubscriber> = (state: IAddSubscriber | undefined, incomingAction: Action): IAddSubscriber => 
{
    if (state === undefined) return CombinedDefaults.addSubscriber;

    const action = incomingAction as TKnownActions;
    switch (action.type) 
    {
        case ADD_SUBSCRIBER_CLEAR:
            return CombinedDefaults.addSubscriber;

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
