import { Action, Reducer } from 'redux';
import { TKnownActions, SELECT_ARTICLE, RESET_SELECTION } from "../Actions/selectArticleActions";
import { ISelectArticleState } from "../State/selectArticleState";

export const SelectArticleReducer: Reducer<ISelectArticleState> = (state: ISelectArticleState | undefined, incomingAction: Action): ISelectArticleState => 
{

    const defaultValues = 
    {
        id: "",
        title: "",
        desc: "",
        status: "",
        likes: 0,
        readCount: 0
    };

    if (state === undefined) return defaultValues;

    const updatedStore = 
    {
        id: state.id,
        title: state.title,
        desc: state.desc,
        status: state.status,
        likes: state.likes,
        readCount: state.readCount
    } 

    const action = incomingAction as TKnownActions;
    switch (action.type) 
    {
        case SELECT_ARTICLE: return updatedStore;
        case RESET_SELECTION: return defaultValues;
        default: return state;
    }

};
