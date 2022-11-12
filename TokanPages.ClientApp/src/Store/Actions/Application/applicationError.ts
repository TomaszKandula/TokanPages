import { DialogType } from "../../../Shared/enums";
import { IApplicationAction } from "../../Configuration";

export const CLEAR = "CLEAR_ERROR";
export const RAISE = "RAISE_ERROR";
interface IClear { type: typeof CLEAR }
interface IRaise { type: typeof RAISE, errorDetails: any, dialogType?: DialogType }
export type TErrorActions = IClear | IRaise;

export const ApplicationErrorAction = 
{
    clear: (): IApplicationAction<TErrorActions> => (dispatch) => 
    {
        dispatch({ type: CLEAR });
    },
    raise: (message: any, dialogType?: DialogType): IApplicationAction<TErrorActions> => (dispatch) => 
    {
        dispatch({ type: RAISE, errorDetails: message, dialogType: dialogType });
    }
}
