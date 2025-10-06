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
                flagImageType: state.flagImageType,
                languages: state.languages,
                warnings: state.warnings,
                pages: state.pages,
                meta: state.meta,
                errorBoundary: state.errorBoundary,
            };
        case SET:
            return {
                id: action.language.id,
                flagImageType: action.language.flagImageType,
                languages: action.language.languages,
                warnings: action.language.warnings,
                pages: action.language.pages,
                meta: action.language.meta,
                errorBoundary: action.language.errorBoundary,
            };
        default:
            return state;
    }
};
