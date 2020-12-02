import { Action, Reducer } from 'redux';
import { RESET_SELECTION, SELECT_ARTICLE, TKnownActions } from 'Redux/Actions/selectArticleActions';
import { IArticle, initArticle } from 'Redux/applicationState';

const SelectArticleReducer: Reducer<IArticle> = (state: IArticle | undefined, incomingAction: Action): IArticle => 
{

    if (state === undefined) return initArticle;

    const updatedStore = 
    {
        id:      state.id,
        title:   state.title,
        desc:    state.desc,
        status:  state.status,
        likes:   state.likes,
        readCount: state.readCount
    } 

    const action = incomingAction as TKnownActions;
    switch (action.type) 
    {
        case SELECT_ARTICLE: return updatedStore;
        case RESET_SELECTION: return initArticle;
        default: return state;
    }

};

export default SelectArticleReducer;
