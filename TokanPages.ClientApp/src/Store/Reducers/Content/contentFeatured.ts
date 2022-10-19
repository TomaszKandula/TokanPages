import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { IContentFeatured } from "../../States";

import { 
    TKnownActions,
    RECEIVE_FEATURED_CONTENT, 
    REQUEST_FEATURED_CONTENT
} from "../../Actions/Content/contentFeatured";

export const ContentFeatured: 
    Reducer<IContentFeatured> = (state: IContentFeatured | undefined, incomingAction: Action): 
    IContentFeatured => 
{
    if (state === undefined) return ApplicationDefault.contentFeatured;

    const action = incomingAction as TKnownActions;
    switch(action.type)
    {
        case REQUEST_FEATURED_CONTENT:
            return { 
                isLoading: true, 
                content: state.content
            };

        case RECEIVE_FEATURED_CONTENT:
            return { 
                isLoading: false, 
                content: action.payload.content
            };

        default: return state;
    }
}
