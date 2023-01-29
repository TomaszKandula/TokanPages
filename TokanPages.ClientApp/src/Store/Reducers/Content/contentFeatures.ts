import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { ContentFeaturesState } from "../../States";

import { 
    TKnownActions,
    RECEIVE, 
    REQUEST
} from "../../Actions/Content/contentFeatures";

export const ContentFeatures: 
    Reducer<ContentFeaturesState> = (state: ContentFeaturesState | undefined, incomingAction: Action): 
    ContentFeaturesState => 
{
    if (state === undefined) return ApplicationDefault.contentFeatures;

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
