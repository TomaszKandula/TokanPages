import { ApplicationAction } from "../../Configuration";
import { ApplicationDialogState } from "../../States";

export const CLEAR = "CLEAR_DIALOG";
export const RAISE = "RAISE_DIALOG";
interface Clear {
    type: typeof CLEAR;
}
interface Raise {
    type: typeof RAISE;
    dialog: ApplicationDialogState;
}
export type TDialogActions = Clear | Raise;

export const ApplicationDialogAction = {
    clear: (): ApplicationAction<TDialogActions> => dispatch => {
        dispatch({ type: CLEAR });
    },
    raise:
        (dialog: ApplicationDialogState): ApplicationAction<TDialogActions> =>
        dispatch => {
            dispatch({ type: RAISE, dialog: dialog });
        },
};
