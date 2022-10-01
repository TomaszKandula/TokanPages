import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { IArticleUpdate } from "../../States";
import { OperationStatus } from "../../../Shared/enums";
import { 
    TKnownActions, 
    UPDATE_ARTICLE, 
    UPDATE_ARTICLE_CLEAR,
    UPDATE_ARTICLE_RESPONSE, 
} from "../../Actions/Articles/articleUpdate";

export const ArticleUpdate: 
    Reducer<IArticleUpdate> = (state: IArticleUpdate | undefined, incomingAction: Action): 
    IArticleUpdate => 
{
    if (state === undefined) return ApplicationDefault.articleUpdate;

    const action = incomingAction as TKnownActions;
    switch (action.type) 
    {
        case UPDATE_ARTICLE_CLEAR:
            return ApplicationDefault.articleUpdate;
        
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