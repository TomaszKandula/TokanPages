import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { IContentFeatured } from "../../States";

import { 
    TKnownActions,
    RECEIVE, 
    REQUEST
} from "../../Actions/Content/contentFeatured";

export const ContentFeatured: 
    Reducer<IContentFeatured> = (state: IContentFeatured | undefined, incomingAction: Action): 
    IContentFeatured => 
{
    if (state === undefined) return ApplicationDefault.contentFeatured;

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
