import { Action, Reducer } from "redux";
import { ApplicationNavbarState } from "../../States";
import { ApplicationDefault } from "../../Configuration";

import { CLEAR, SET, TNavbarActions } from "../../Actions/Application/applicationNavbar";

export const ApplicationNavbar: Reducer<ApplicationNavbarState> = (
    state: ApplicationNavbarState | undefined,
    incomingAction: Action
): ApplicationNavbarState => {
    if (state === undefined) return ApplicationDefault.applicationNavbar;

    const action = incomingAction as TNavbarActions;
    switch (action.type) {
        case CLEAR:
            return {
                selection: undefined,
                name: undefined,
                path: undefined,
            };
        case SET:
            return {
                selection: action.menu.selection,
                name: action.menu.name,
                path: action.menu.path,
            };
        default:
            return state;
    }
};
