import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { ContentUpdateSubscriberState } from "../../States";

import { 
    TKnownActions,
    RECEIVE, 
    REQUEST
} from "../../Actions/Content/contentUpdateSubscriber";

export const ContentUpdateSubscriber: 
    Reducer<ContentUpdateSubscriberState> = (state: ContentUpdateSubscriberState | undefined, incomingAction: Action): 
    ContentUpdateSubscriberState => 
{
    if (state === undefined) return ApplicationDefault.contentUpdateSubscriber;

    const action = incomingAction as TKnownActions;
    switch(action.type)
    {
        case REQUEST:
            return { 
                isLoading: true, 
                content: state.content
            };

        case RECEIVE:
            return { 
                isLoading: false, 
                content: action.payload.content
            };

        default: return state;
    }
}
