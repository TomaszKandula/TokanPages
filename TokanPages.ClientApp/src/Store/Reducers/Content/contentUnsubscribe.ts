import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { ContentUnsubscribeState } from "../../States";

import { 
    TKnownActions,
    RECEIVE, 
    REQUEST
} from "../../Actions/Content/contentUnsubscribe";

export const ContentUnsubscribe: 
    Reducer<ContentUnsubscribeState> = (state: ContentUnsubscribeState | undefined, incomingAction: Action): 
    ContentUnsubscribeState => 
{
    if (state === undefined) return ApplicationDefault.contentUnsubscribe;

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
