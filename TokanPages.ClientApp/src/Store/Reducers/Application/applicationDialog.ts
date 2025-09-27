import { Action, Reducer } from "redux";
import { ApplicationDialogState } from "../../States";
import { ApplicationDefault } from "../../Configuration";

import { CLEAR, RAISE, TDialogActions } from "../../Actions/Application/applicationDialog";

export const ApplicationDialog: Reducer<ApplicationDialogState> = (
    state: ApplicationDialogState | undefined,
    incomingAction: Action
): ApplicationDialogState => {
    if (state === undefined) return ApplicationDefault.applicationDialog;

    const action = incomingAction as TDialogActions;
    switch (action.type) {
        case CLEAR:
            return {
                title: undefined,
                message: undefined,
                validation: undefined,
                icon: undefined,
                buttons: undefined,
            };
        case RAISE:
            return {
                title: action.dialog.title,
                message: action.dialog.message,
                validation: action.dialog.validation,
                icon: action.dialog.icon,
                buttons: action.dialog.buttons,
            };
        default:
            return state;
    }
};
