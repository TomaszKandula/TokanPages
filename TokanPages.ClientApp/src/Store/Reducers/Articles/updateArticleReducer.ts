import { Action, Reducer } from "redux";
import { combinedDefaults } from "../../combinedDefaults";
import { IUpdateArticle } from "../../States/Articles/updateArticleState";
import { OperationStatus } from "../../../Shared/enums";
import { 
    TKnownActions, 
    UPDATE_ARTICLE, 
    UPDATE_ARTICLE_CLEAR,
    UPDATE_ARTICLE_RESPONSE, 
} from "../../Actions/Articles/updateArticleAction";

export const UpdateArticleReducer: Reducer<IUpdateArticle> = (state: IUpdateArticle | undefined, incomingAction: Action): IUpdateArticle => 
{
    if (state === undefined) return combinedDefaults.updateArticle;

    const action = incomingAction as TKnownActions;
    switch (action.type) 
    {
        case UPDATE_ARTICLE_CLEAR:
            return combinedDefaults.updateArticle;
        
        case UPDATE_ARTICLE:
            return { 
                operationStatus: OperationStatus.inProgress, 
                attachedErrorObject: state.attachedErrorObject
            };

        case UPDATE_ARTICLE_RESPONSE:
            return { 
                operationStatus: OperationStatus.hasFinished, 
                attachedErrorObject: { } 
            };

        default: return state;
    }
};
