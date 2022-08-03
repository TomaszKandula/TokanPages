import { Action, Reducer } from "redux";
import { IUserLanguage } from "../../Redux/States/userLanguageState";
import { combinedDefaults } from "../../Redux/combinedDefaults";
import { SET_ANOTHER_LANGUAGE, SET_DEFAULT_LANGUAGE, TKnownActions } from "../../Redux/Actions/userLanguageAction";

export const UserLanguageReducer: Reducer<IUserLanguage> = (state: IUserLanguage | undefined, incomingAction: Action): IUserLanguage =>
{
    if (state === undefined) return combinedDefaults.userLanguage;

    const action = incomingAction as TKnownActions;
    switch(action.type)
    {
        case SET_DEFAULT_LANGUAGE:
            return {
                id: state.id
            }
        case SET_ANOTHER_LANGUAGE:
            return { 
                id: action.languageId
            }
        default: return state;
    }
}
