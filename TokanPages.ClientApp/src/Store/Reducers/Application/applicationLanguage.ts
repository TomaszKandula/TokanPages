import { Action, Reducer } from "redux";
import { ApplicationLanguageState } from "../../States";
import { ApplicationDefault } from "../../Configuration";

import { SET, RESET, TKnownActions } from "../../Actions/Application/applicationLanguage";

export const ApplicationLanguage: Reducer<ApplicationLanguageState> = (
    state: ApplicationLanguageState | undefined,
    incomingAction: Action
): ApplicationLanguageState => {
    if (state === undefined) return ApplicationDefault.applicationLanguage;

    const action = incomingAction as TKnownActions;
    switch (action.type) {
        case RESET:
            return {
                id: state.id,
                languages: state.languages,
            };
        case SET:
            return {
                id: action.language.id,
                languages: action.language.languages,
            };
        default:
            return state;
    }
};
