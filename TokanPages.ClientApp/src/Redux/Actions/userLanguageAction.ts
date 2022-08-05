import { AppThunkAction } from "../applicationState";

export const SET_DEFAULT_LANGUAGE = "SET_DEFAULT_LANGUAGE";
export const SET_ANOTHER_LANGUAGE = "SET_ANOTHER_LANGUAGE";

export interface ISetDefaultLanguage { type: typeof SET_DEFAULT_LANGUAGE }
export interface ISetAnotherLanguage { type: typeof SET_ANOTHER_LANGUAGE, languageId: string }

export type TKnownActions = ISetDefaultLanguage | ISetAnotherLanguage;

export const ActionCreators = 
{
    setDefaultLanguage: (): AppThunkAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: SET_DEFAULT_LANGUAGE });
    },
    setAnotherLanguage: (id: string): AppThunkAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: SET_ANOTHER_LANGUAGE, languageId: id });
    }
}
