import { ISelectArticleState } from "./State/selectArticleState";
import { IListArticlesState } from "./State/listArticlesState";
import { SelectArticleReducer} from "./Reducers/selectArticleReducer";
import { ListArticlesReducer } from "./Reducers/listArticlesReducer";

// The top-level state object
export interface IApplicationState 
{
    selectArticle: ISelectArticleState | undefined;
    listArticles: IListArticlesState | undefined;
}

// Whenever an action is dispatched, Redux will update each top-level application state property using
// the reducer with the matching name. It is important that the names match exactly, and that the reducer
// acts on the corresponding IApplicationState property type.
export const reducers = 
{
    selectArticle: SelectArticleReducer,
    listArticles: ListArticlesReducer
};

// This type can be used as a hint on action creators so that its 'dispatch' and 'getState' params are
// correctly typed to match Redux store.
export interface AppThunkAction<TAction> 
{
    (dispatch: (action: TAction) => void, getState: () => IApplicationState): void;
}
