import { Action, Reducer } from "redux";
import { CombinedDefaults } from "../../Configuration";
import { IUpdateArticle } from "../../States";
import { OperationStatus } from "../../../Shared/enums";
import { 
    TKnownActions, 
    UPDATE_ARTICLE, 
    UPDATE_ARTICLE_CLEAR,
    UPDATE_ARTICLE_RESPONSE, 
} from "../../Actions/Articles/updateArticleAction";

export const UpdateArticleReducer: 
    Reducer<IUpdateArticle> = (state: IUpdateArticle | undefined, incomingAction: Action): 
    IUpdateArticle => 
{
    if (state === undefined) return CombinedDefaults.updateArticle;

    const action = incomingAction as TKnownActions;
    switch (action.type) 
    {
        case UPDATE_ARTICLE_CLEAR:
            return CombinedDefaults.updateArticle;
        
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
