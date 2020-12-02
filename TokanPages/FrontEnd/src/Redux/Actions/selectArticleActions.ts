export const SELECT_ARTICLE  = "SELECT_ARTICLE";
export const RESET_SELECTION = "RESET_SELECTION";

export interface ISelectArticle  { type: typeof SELECT_ARTICLE }
export interface IResetSelection { type: typeof RESET_SELECTION }

export type TKnownActions = ISelectArticle | IResetSelection;

export const ActionCreators = 
{
    select: () => ({ type: SELECT_ARTICLE } as ISelectArticle),
    reset: () => ({ type: RESET_SELECTION } as IResetSelection)
};
