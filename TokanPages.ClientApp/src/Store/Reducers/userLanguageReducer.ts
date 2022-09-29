import { Action, Reducer } from "redux";
import { IUserLanguage } from "../States";
import { CombinedDefaults } from "../Configuration";
import { SET_LANGUAGE, SET_DEFAULT_LANGUAGE, TKnownActions } from "../Actions/userLanguageAction";

export const UserLanguageReducer: 
    Reducer<IUserLanguage> = (state: IUserLanguage | undefined, incomingAction: Action): 
    IUserLanguage =>
{
    if (state === undefined) return CombinedDefaults.userLanguage;

    const action = incomingAction as TKnownActions;
    switch(action.type)
    {
        case SET_DEFAULT_LANGUAGE:
            return {
                id: state.id,
                languages: state.languages
            }
        case SET_LANGUAGE:
            return { 
                id: action.language.id,
                languages: action.language.languages
            }
        default: return state;
    }
}
