import { AppThunkAction } from "../applicationState";

export const CLEAR_ERROR = "CLEAR_ERROR";
export const RAISE_ERROR = "RAISE_ERROR";

export interface IClearError { type: typeof CLEAR_ERROR }
export interface IRaiseError { type: typeof RAISE_ERROR, errorObject: any }

export type TErrorActions = IClearError | IRaiseError;

export const ActionCreators = 
{
    clearError: ():  AppThunkAction<TErrorActions> => async (dispatch) => 
    {
        dispatch({ type: CLEAR_ERROR });
    },
    raiseError: (error: any):  AppThunkAction<TErrorActions> => async (dispatch) => 
    {
        dispatch({ type: RAISE_ERROR, errorObject: error });
    }
}
