import { Action, Reducer } from "redux";
import { ApplicationDefaults } from "../../Configuration";
import { IContentUpdateSubscriber } from "../../States";
import { 
    TKnownActions,
    RECEIVE_UPDATE_SUBSCRIBER_CONTENT, 
    REQUEST_UPDATE_SUBSCRIBER_CONTENT
} from "../../Actions/Content/getUpdateSubscriberContentAction";

export const ContentUpdateSubscriber: 
    Reducer<IContentUpdateSubscriber> = (state: IContentUpdateSubscriber | undefined, incomingAction: Action): 
    IContentUpdateSubscriber => 
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
