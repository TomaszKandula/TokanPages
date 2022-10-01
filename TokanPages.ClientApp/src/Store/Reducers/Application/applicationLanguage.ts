import { Action, Reducer } from "redux";
import { IApplicationLanguage } from "../../States";
import { ApplicationDefaults } from "../../Configuration";
import { SET_LANGUAGE, SET_DEFAULT_LANGUAGE, TKnownActions } from "../../Actions/Application/applicationLanguage";

export const ApplicationLanguage: 
    Reducer<IApplicationLanguage> = (state: IApplicationLanguage | undefined, incomingAction: Action): 
    IApplicationLanguage =>
{
    if (state === undefined) return ApplicationDefaults.applicationLanguage;

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
