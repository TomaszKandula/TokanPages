import { IApplicationAction } from "../../Configuration";
import { IApplicationLanguage } from "../../States";

export const RESET = "RESET_LANGUAGE";
export const SET = "SET_LANGUAGE";
interface IReset { type: typeof RESET }
interface ISet { type: typeof SET, language: IApplicationLanguage }
export type TKnownActions = IReset | ISet;

export const ApplicationLanguageAction = 
{
    reset: (): IApplicationAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: RESET });
    },
    set: (language: IApplicationLanguage): IApplicationAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: SET, language: language });
    }
}
