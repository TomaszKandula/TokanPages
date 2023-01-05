import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { IArticleSelection } from "../../States";

import { 
    TKnownActions, 
    REQUEST, 
    RECEIVE, 
    RESET, 
} from "../../Actions/Articles/articleSelection";

export const ArticleSelection: 
    Reducer<IArticleSelection> = (state: IArticleSelection | undefined, incomingAction: Action): 
    IArticleSelection => 
{
    if (state === undefined) return ApplicationDefault.articleSelection;

    const action = incomingAction as TKnownActions;
    switch (action.type) 
    {
        case RESET:
            return ApplicationDefault.articleSelection;

        case REQUEST:
            return { 
                isLoading: true, 
                article: state.article 
            };

        case RECEIVE:
            return { 
                isLoading: false, 
                article: action.payload
            };
        
        default: return state;
    }
};
