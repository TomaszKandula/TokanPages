import { Action, Reducer } from "redux";
import { combinedDefaults } from "../combinedDefaults";
import { IArticle } from "../../Redux/States/selectArticleState";
import { 
    TKnownActions, 
    REQUEST_ARTICLE, 
    RECEIVE_ARTICLE, 
    RESET_SELECTION, 
} from "../Actions/selectArticleAction";

const SelectArticleReducer: Reducer<IArticle> = (state: IArticle | undefined, incomingAction: Action): IArticle => 
{
    if (state === undefined) return combinedDefaults.selectArticle;

    const action = incomingAction as TKnownActions;
    switch (action.type) 
    {
        case RESET_SELECTION:
            return combinedDefaults.selectArticle;

        case REQUEST_ARTICLE:
            return { 
                isLoading: true, 
                article: state.article, 
                attachedErrorObject: state.attachedErrorObject 
            };

        case RECEIVE_ARTICLE:
            return { 
                isLoading: false, 
                article: action.payload, 
                attachedErrorObject: { } 
            };
        
        default: return state;
    }
};

export default SelectArticleReducer;
