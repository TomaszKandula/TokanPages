import { DialogType } from "Shared/enums";
import { AppThunkAction } from "../applicationState";

export const CLEAR_ERROR = "CLEAR_ERROR";
export const RAISE_ERROR = "RAISE_ERROR";

export interface IClearError { type: typeof CLEAR_ERROR }
export interface IRaiseError { type: typeof RAISE_ERROR, errorObject: any, dialogType?: DialogType }

export type TErrorActions = IClearError | IRaiseError;

export const ActionCreators = 
{
    clearError: (): AppThunkAction<TErrorActions> => (dispatch) => 
    {
        dispatch({ type: CLEAR_ERROR });
    },
    raiseError: (errorMessage: any, dialogType?: DialogType): AppThunkAction<TErrorActions> => (dispatch) => 
    {
        dispatch({ type: RAISE_ERROR, errorObject: errorMessage, dialogType: dialogType });
    }
}
