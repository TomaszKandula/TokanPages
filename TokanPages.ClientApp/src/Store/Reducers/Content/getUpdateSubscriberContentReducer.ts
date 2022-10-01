import { Action, Reducer } from "redux";
import { ApplicationDefaults } from "../../Configuration";
import { IGetUpdateSubscriberContent } from "../../States";
import { 
    TKnownActions,
    RECEIVE_UPDATE_SUBSCRIBER_CONTENT, 
    REQUEST_UPDATE_SUBSCRIBER_CONTENT
} from "../../Actions/Content/getUpdateSubscriberContentAction";

export const GetUpdateSubscriberContentReducer: 
    Reducer<IGetUpdateSubscriberContent> = (state: IGetUpdateSubscriberContent | undefined, incomingAction: Action): 
    IGetUpdateSubscriberContent => 
{
    if (state === undefined) return ApplicationDefaults.contentUpdateSubscriber;

    const action = incomingAction as TKnownActions;
    switch(action.type)
    {
        case REQUEST_UPDATE_SUBSCRIBER_CONTENT:
            return { 
                isLoading: true, 
                content: state.content
            };

        case RECEIVE_UPDATE_SUBSCRIBER_CONTENT:
            return { 
                isLoading: false, 
                content: action.payload.content
            };

        default: return state;
    }
}
