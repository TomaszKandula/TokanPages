import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { ISubscriberAdd } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

import { 
    TKnownActions, 
    ADD, 
    RESPONSE, 
    CLEAR, 
} from "../../Actions/Subscribers/subscriberAdd";

export const SubscriberAdd: 
    Reducer<ISubscriberAdd> = (state: ISubscriberAdd | undefined, incomingAction: Action): 
    ISubscriberAdd => 
{
    if (state === undefined) return ApplicationDefault.subscriberAdd;

    const action = incomingAction as TKnownActions;
    switch (action.type) 
    {
        case CLEAR:
            return ApplicationDefault.subscriberAdd;

        case ADD:
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
