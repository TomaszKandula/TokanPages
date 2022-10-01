import { Action, Reducer } from "redux";
import { ApplicationDefaults } from "../../Configuration";
import { IArticle } from "../../States";
import { 
    TKnownActions, 
    REQUEST_ARTICLE, 
    RECEIVE_ARTICLE, 
    RESET_SELECTION, 
} from "../../Actions/Articles/selectArticleAction";

export const SelectArticleReducer: 
    Reducer<IArticle> = (state: IArticle | undefined, incomingAction: Action): 
    IArticle => 
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