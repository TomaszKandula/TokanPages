import { Action, Reducer } from "redux";
import { TKnownActions, REQUEST_ARTICLE, RECEIVE_ARTICLE, RESET_SELECTION } from "../../Redux/Actions/selectArticleActions";
import { IArticle, ArticleDefaultValues } from "../../Redux/applicationState";

const SelectArticleReducer: Reducer<IArticle> = (state: IArticle | undefined, incomingAction: Action): IArticle => 
{
    if (state === undefined) return ArticleDefaultValues;

    const action = incomingAction as TKnownActions;
    switch (action.type) 
    {
        case RESET_SELECTION:
            return ArticleDefaultValues;

        case REQUEST_ARTICLE:
            return { isLoading: true, article: state.article };

        case RECEIVE_ARTICLE:
            return { isLoading: false, article: action.payload };
        
        default: return state;
    }
};

export default SelectArticleReducer;
