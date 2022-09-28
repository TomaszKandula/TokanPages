import { AppThunkAction } from "../applicationState";
import { IRaiseDialog } from "../States/raiseDialogState";

export const CLEAR_DIALOG = "CLEAR_DIALOG";
export const RAISE_DIALOG = "RAISE_DIALOG";

export interface IClearDialogBox { type: typeof CLEAR_DIALOG }
export interface IRaiseDialogBox { type: typeof RAISE_DIALOG, dialog: IRaiseDialog }

export type TDialogActions = IClearDialogBox | IRaiseDialogBox;

export const ActionCreators = 
{
    clearDialog: (): AppThunkAction<TDialogActions> => (dispatch) => 
    {
        dispatch({ type: CLEAR_DIALOG });
    },
    raiseDialog: (dialog: IRaiseDialog): AppThunkAction<TDialogActions> => (dispatch) => 
    {
        dispatch({ type: RAISE_DIALOG, dialog: dialog });
    }
}
