import { AppThunkAction } from "../../Configuration";
import { IApplicationDialog } from "../../States";

export const CLEAR_DIALOG = "CLEAR_DIALOG";
export const RAISE_DIALOG = "RAISE_DIALOG";

export interface IClearDialogBox { type: typeof CLEAR_DIALOG }
export interface IRaiseDialogBox { type: typeof RAISE_DIALOG, dialog: IApplicationDialog }

export type TDialogActions = IClearDialogBox | IRaiseDialogBox;

export const ActionCreators = 
{
    clear: (): AppThunkAction<TDialogActions> => (dispatch) => 
    {
        dispatch({ type: CLEAR_DIALOG });
    },
    raise: (dialog: IApplicationDialog): AppThunkAction<TDialogActions> => (dispatch) => 
    {
        dispatch({ type: RAISE_DIALOG, dialog: dialog });
    }
}
