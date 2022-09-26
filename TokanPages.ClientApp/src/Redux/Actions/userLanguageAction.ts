import { AppThunkAction } from "../applicationState";
import { IUserLanguage } from "../../Redux/States/userLanguageState";

export const SET_DEFAULT_LANGUAGE = "SET_DEFAULT_LANGUAGE";
export const SET_LANGUAGE = "SET_LANGUAGE";

export interface ISetDefaultLanguage { type: typeof SET_DEFAULT_LANGUAGE }
export interface ISetLanguage { type: typeof SET_LANGUAGE, language: IUserLanguage }

export type TKnownActions = ISetDefaultLanguage | ISetLanguage;

export const ActionCreators = 
{
    setDefaultLanguage: (): AppThunkAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: SET_DEFAULT_LANGUAGE });
    },
    setLanguage: (language: IUserLanguage): AppThunkAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: SET_LANGUAGE, language: language });
    }
}
