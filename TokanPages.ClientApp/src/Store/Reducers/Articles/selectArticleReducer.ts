import { Action, Reducer } from "redux";
import { CombinedDefaults } from "../../Configuration";
import { IArticle } from "../../States/Articles/selectArticleState";
import { 
    TKnownActions, 
    REQUEST_ARTICLE, 
    RECEIVE_ARTICLE, 
    RESET_SELECTION, 
} from "../../Actions/Articles/selectArticleAction";

export const SelectArticleReducer: Reducer<IArticle> = (state: IArticle | undefined, incomingAction: Action): IArticle => 
{
    if (state === undefined) return CombinedDefaults.selectArticle;

    const action = incomingAction as TKnownActions;
    switch (action.type) 
    {
        case RESET_SELECTION:
            return CombinedDefaults.selectArticle;

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
