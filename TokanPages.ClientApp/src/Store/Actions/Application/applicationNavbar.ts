import { ApplicationAction } from "../../Configuration";
import { ApplicationNavbarState } from "../../States";

export const CLEAR = "CLEAR_NAVBAR_MENU";
export const SET = "SET_NAVBAR_MENU";
interface Clear {
    type: typeof CLEAR;
}
interface Set {
    type: typeof SET;
    menu: ApplicationNavbarState;
}
export type TNavbarActions = Clear | Set;

export const ApplicationNavbarAction = {
    clear: (): ApplicationAction<TNavbarActions> => dispatch => {
        dispatch({ type: CLEAR });
    },
    set:
        (menu: ApplicationNavbarState): ApplicationAction<TNavbarActions> =>
        dispatch => {
            dispatch({ type: SET, menu: menu });
        },
};
