import { AppThunkAction, IArticle } from "../../Redux/applicationState";

export const SELECT_ARTICLE  = "SELECT_ARTICLE";
export const RESET_SELECTION = "RESET_SELECTION";

export interface ISelectArticle  { type: typeof SELECT_ARTICLE, payload: IArticle }
export interface IResetSelection { type: typeof RESET_SELECTION }

export type TKnownActions = ISelectArticle | IResetSelection;

export const ActionCreators = 
{
    resetSelection: () => ({ type: RESET_SELECTION } as IResetSelection),
    selectArticle: (id: string): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        const appState = getState();
        const articlesList = appState.listArticles.articles;

        for (let item of articlesList)
        {
            if (item.id === id)
            {
                dispatch({ type: SELECT_ARTICLE, payload: item });
                break;
            }
        }
    }
};
