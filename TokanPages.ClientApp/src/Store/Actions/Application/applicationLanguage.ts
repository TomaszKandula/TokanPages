import { IApplicationAction } from "../../Configuration";
import { IApplicationLanguage } from "../../States";

export const RESET_LANGUAGE = "RESET_LANGUAGE";
export const SET_LANGUAGE = "SET_LANGUAGE";

export interface IResetLanguage { type: typeof RESET_LANGUAGE }
export interface ISetLanguage { type: typeof SET_LANGUAGE, language: IApplicationLanguage }

export type TKnownActions = IResetLanguage | ISetLanguage;

export const ApplicationLanguageAction = 
{
    reset: (): IApplicationAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: RESET_LANGUAGE });
    },
    set: (language: IApplicationLanguage): IApplicationAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: SET_LANGUAGE, language: language });
    }
}
