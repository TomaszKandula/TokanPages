import { Action, Reducer } from "redux";
import { IApplicationLanguage } from "../../States";
import { ApplicationDefault } from "../../Configuration";
import { SET_LANGUAGE, RESET_LANGUAGE, TKnownActions } from "../../Actions/Application/applicationLanguage";

export const ApplicationLanguage: 
    Reducer<IApplicationLanguage> = (state: IApplicationLanguage | undefined, incomingAction: Action): 
    IApplicationLanguage =>
{
    if (state === undefined) return ApplicationDefault.applicationLanguage;

    const action = incomingAction as TKnownActions;
    switch(action.type)
    {
        case RESET_LANGUAGE:
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
