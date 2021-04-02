import { Action, Reducer } from "redux";
import { TKnownActions, API_UPDATE_ARTICLE, API_UPDATE_ARTICLE_RESPONSE } from "../Actions/updateArticleActions";
import { UpdateArticleDefaultValues, IUpdateArticle } from "../applicationState";

const UpdateArticleReducer: Reducer<IUpdateArticle> = (state: IUpdateArticle | undefined, incomingAction: Action): IUpdateArticle => 
{
    if (state === undefined) return UpdateArticleDefaultValues;

    const action = incomingAction as TKnownActions;
    switch (action.type) 
    {
        case API_UPDATE_ARTICLE:
            return { isUpdatingArticle: true, hasUpdatedArticle: state.hasUpdatedArticle };

        case API_UPDATE_ARTICLE_RESPONSE:
            return { isUpdatingArticle: false, hasUpdatedArticle: action.hasUpdatedArticle };

        default: return state;
    }
};

export default UpdateArticleReducer;
