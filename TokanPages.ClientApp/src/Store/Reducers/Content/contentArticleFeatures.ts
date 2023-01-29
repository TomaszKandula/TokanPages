import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { ContentArticleFeaturesState } from "../../States";

import { 
    TKnownActions,
    RECEIVE, 
    REQUEST
} from "../../Actions/Content/contentArticleFeatures";

export const ContentArticleFeatures: 
    Reducer<ContentArticleFeaturesState> = (state: ContentArticleFeaturesState | undefined, incomingAction: Action): 
    ContentArticleFeaturesState => 
{
    if (state === undefined) return ApplicationDefault.contentArticleFeatures;

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
