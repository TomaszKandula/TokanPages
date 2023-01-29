import { DialogType } from "../../../Shared/enums";
import { ApplicationAction } from "../../Configuration";

export const CLEAR = "CLEAR_ERROR";
export const RAISE = "RAISE_ERROR";
interface Clear { type: typeof CLEAR }
interface Raise { type: typeof RAISE, errorDetails: any, dialogType?: DialogType }
export type TErrorActions = Clear | Raise;

export const ApplicationErrorAction = 
{
    clear: (): ApplicationAction<TErrorActions> => (dispatch) => 
    {
        dispatch({ type: CLEAR });
    },
    raise: (message: any, dialogType?: DialogType): ApplicationAction<TErrorActions> => (dispatch) => 
    {
        dispatch({ type: RAISE, errorDetails: message, dialogType: dialogType });
    }
}
