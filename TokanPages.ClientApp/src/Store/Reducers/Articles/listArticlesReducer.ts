import { Action, Reducer } from "redux";
import { CombinedDefaults } from "../../Configuration";
import { IArticles } from "../../States/Articles/listArticlesState";
import { 
    TKnownActions, 
    RECEIVE_ARTICLES, 
    REQUEST_ARTICLES, 
} from "../../Actions/Articles/listArticlesAction";

export const ListArticlesReducer: Reducer<IArticles> = (state: IArticles | undefined, incomingAction: Action): IArticles => 
{
    if (state === undefined) return CombinedDefaults.listArticles;

    const action = incomingAction as TKnownActions;
    switch(action.type)
    {
        case REQUEST_ARTICLES:
            return { 
                isLoading: true, 
                articles: state.articles
            };

        case RECEIVE_ARTICLES:
            return { 
                isLoading: false, 
                articles: action.payload
            };

        default: return state;
    }
}
