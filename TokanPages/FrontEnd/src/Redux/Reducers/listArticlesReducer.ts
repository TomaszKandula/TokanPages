import { Action, Reducer } from 'redux';
import { TKnownActions, RECEIVE_ARTICLES, REQUEST_ARTICLES } from 'Redux/Actions/listArticlesActions';
import { IListArticlesState } from 'Redux/State/listArticlesState';

export const ListArticlesReducer: Reducer<IListArticlesState> = (state: IListArticlesState | undefined, incomingAction: Action): IListArticlesState => 
{

    if (state === undefined) 
    {
        return { articles: [], isLoading: false };
    }

    const action = incomingAction as TKnownActions;
    switch(action.type)
    {

        case REQUEST_ARTICLES:
            return { isLoading: true, articles: state.articles };

        case RECEIVE_ARTICLES:
            return { isLoading: false, articles: action.data };

        default: return state;

    }

}
