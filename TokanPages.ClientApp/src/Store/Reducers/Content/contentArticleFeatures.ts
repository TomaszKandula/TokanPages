import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { IContentArticleFeatures } from "../../States";

import { 
    TKnownActions,
    RECEIVE, 
    REQUEST
} from "../../Actions/Content/contentArticleFeatures";

export const ContentArticleFeatures: 
    Reducer<IContentArticleFeatures> = (state: IContentArticleFeatures | undefined, incomingAction: Action): 
    IContentArticleFeatures => 
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
