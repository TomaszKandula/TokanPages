import { Action, Reducer } from "redux";
import { RESET_SELECTION, SELECT_ARTICLE, TKnownActions } from "Redux/Actions/selectArticleActions";
import { IArticle, ArticleDefaultValues } from "Redux/applicationState";

const SelectArticleReducer: Reducer<IArticle> = (state: IArticle | undefined, incomingAction: Action): IArticle => 
{

    if (state === undefined) return ArticleDefaultValues;

    const action = incomingAction as TKnownActions;
    switch (action.type) 
    {
        case RESET_SELECTION: return ArticleDefaultValues;
        case SELECT_ARTICLE: return {  
            id: action.payload.id,
            title: action.payload.title,
            description: action.payload.description,
            isPublished: action.payload.isPublished,
            likes: action.payload.likes,
            readCount: action.payload.readCount,
            createdAt: action.payload.createdAt,
            updatedAt: action.payload.updatedAt
        };
        default: return state;
    }

};

export default SelectArticleReducer;
