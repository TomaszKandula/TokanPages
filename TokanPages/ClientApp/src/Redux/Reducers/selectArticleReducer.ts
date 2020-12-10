import { Action, Reducer } from 'redux';
import { RESET_SELECTION, SELECT_ARTICLE, TKnownActions } from 'Redux/Actions/selectArticleActions';
import { IArticle, initArticle } from 'Redux/applicationState';

const SelectArticleReducer: Reducer<IArticle> = (state: IArticle | undefined, incomingAction: Action): IArticle => 
{

    if (state === undefined) return initArticle;

    const action = incomingAction as TKnownActions;
    switch (action.type) 
    {
        case RESET_SELECTION: return initArticle;
        case SELECT_ARTICLE: return {  
            id:        action.payload.id,
            title:     action.payload.title,
            desc:      action.payload.desc,
            status:    action.payload.status,
            likes:     action.payload.likes,
            readCount: action.payload.readCount
        };
        default: return state;
    }

};

export default SelectArticleReducer;
