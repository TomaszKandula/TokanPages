import { Action, Reducer } from "redux";
import { combinedDefaults } from "../combinedDefaults";
import { IUpdateArticle } from "../../Redux/States/updateArticleState";
import { 
    TKnownActions, 
    API_UPDATE_ARTICLE, 
    API_UPDATE_ARTICLE_RESPONSE, 
} from "../Actions/updateArticleActions";

const UpdateArticleReducer: Reducer<IUpdateArticle> = (state: IUpdateArticle | undefined, incomingAction: Action): IUpdateArticle => 
{
    if (state === undefined) return combinedDefaults.updateArticle;

    const action = incomingAction as TKnownActions;
    switch (action.type) 
    {
        case API_UPDATE_ARTICLE:
            return { 
                isUpdatingArticle: true, 
                hasUpdatedArticle: state.hasUpdatedArticle,
                attachedErrorObject: state.attachedErrorObject
            };

        case API_UPDATE_ARTICLE_RESPONSE:
            return { 
                isUpdatingArticle: false, 
                hasUpdatedArticle: action.hasUpdatedArticle,
                attachedErrorObject: { } 
            };

        default: return state;
    }
};

export default UpdateArticleReducer;
