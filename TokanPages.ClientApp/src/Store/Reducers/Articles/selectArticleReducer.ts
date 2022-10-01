import { Action, Reducer } from "redux";
import { ApplicationDefaults } from "../../Configuration";
import { IArticleSelection } from "../../States";
import { 
    TKnownActions, 
    REQUEST_ARTICLE, 
    RECEIVE_ARTICLE, 
    RESET_SELECTION, 
} from "../../Actions/Articles/selectArticleAction";

export const ArticleSelection: 
    Reducer<IArticleSelection> = (state: IArticleSelection | undefined, incomingAction: Action): 
    IArticleSelection => 
{
    if (state === undefined) return ApplicationDefaults.articleSelection;

    const action = incomingAction as TKnownActions;
    switch (action.type) 
    {
        case RESET_SELECTION:
            return ApplicationDefaults.articleSelection;

        case REQUEST_ARTICLE:
            return { 
                isLoading: true, 
                article: state.article 
            };

        case RECEIVE_ARTICLE:
            return { 
                isLoading: false, 
                article: action.payload
            };
        
        default: return state;
    }
};
