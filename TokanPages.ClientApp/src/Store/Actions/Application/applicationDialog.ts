import { IApplicationAction } from "../../Configuration";
import { IApplicationDialog } from "../../States";

export const CLEAR = "CLEAR_DIALOG";
export const RAISE = "RAISE_DIALOG";
interface IClear { type: typeof CLEAR }
interface IRaise { type: typeof RAISE, dialog: IApplicationDialog }
export type TDialogActions = IClear | IRaise;

export const ApplicationDialogAction = 
{
    clear: (): IApplicationAction<TDialogActions> => (dispatch) => 
    {
        dispatch({ type: CLEAR });
    },
    raise: (dialog: IApplicationDialog): IApplicationAction<TDialogActions> => (dispatch) => 
    {
        dispatch({ type: RAISE, dialog: dialog });
    }
}
