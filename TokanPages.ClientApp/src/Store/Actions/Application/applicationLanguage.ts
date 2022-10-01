import { AppThunkAction } from "../../Configuration";
import { IApplicationLanguage } from "../../States";

export const SET_DEFAULT_LANGUAGE = "SET_DEFAULT_LANGUAGE";
export const SET_LANGUAGE = "SET_LANGUAGE";

export interface ISetDefaultLanguage { type: typeof SET_DEFAULT_LANGUAGE }
export interface ISetLanguage { type: typeof SET_LANGUAGE, language: IApplicationLanguage }

export type TKnownActions = ISetDefaultLanguage | ISetLanguage;

export const ApplicationLanguageAction = 
{
    revert: (): AppThunkAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: SET_DEFAULT_LANGUAGE });
    },
    set: (language: IApplicationLanguage): AppThunkAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: SET_LANGUAGE, language: language });
    }
}
