import { Action, Reducer } from "redux";
import { combinedDefaults } from "../../Redux/Defaults/combinedDefaults";
import { IArticles } from "../../Redux/States/listArticlesState";
import { TKnownActions, RECEIVE_ARTICLES, REQUEST_ARTICLES } from "../../Redux/Actions/listArticlesActions";

const ListArticlesReducer: Reducer<IArticles> = (state: IArticles | undefined, incomingAction: Action): IArticles => 
{
    if (state === undefined) return combinedDefaults.listArticles;

    const action = incomingAction as TKnownActions;
    switch(action.type)
    {
        case REQUEST_ARTICLES:
            return { isLoading: true, articles: state.articles };

        case RECEIVE_ARTICLES:
            return { isLoading: false, articles: action.payload };

        default: return state;
    }
}

export default ListArticlesReducer;
