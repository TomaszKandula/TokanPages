import { ApplicationAction } from "../../Configuration";
import { ApplicationLanguageState } from "../../States";

export const RESET = "RESET_LANGUAGE";
export const SET = "SET_LANGUAGE";
interface Reset {
    type: typeof RESET;
}
interface Set {
    type: typeof SET;
    language: ApplicationLanguageState;
}
export type TKnownActions = Reset | Set;

export const ApplicationLanguageAction = {
    reset: (): ApplicationAction<TKnownActions> => dispatch => {
        dispatch({ type: RESET });
    },
    set:
        (language: ApplicationLanguageState): ApplicationAction<TKnownActions> =>
        dispatch => {
            dispatch({ type: SET, language: language });
        },
};
