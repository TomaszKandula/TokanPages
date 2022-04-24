import { IAuthenticateUserResultDto } from "../../../Api/Models";
import { AppThunkAction } from "../../applicationState";

export const SHOW_USERDATA = "SHOW_USERDATA";
export const CLEAR_USERDATA = "CLEAR_USERDATA";
export const UPDATE_USERDATA = "UPDATE_USERDATA";
export interface IShowUserData { type: typeof SHOW_USERDATA, payload: boolean }
export interface IClearUserData { type: typeof CLEAR_USERDATA }
export interface IUpdateUserData { type: typeof UPDATE_USERDATA, payload: IAuthenticateUserResultDto }
export type TKnownActions = IShowUserData | IClearUserData | IUpdateUserData;

export const ActionCreators = 
{
    show: (isShown: boolean): AppThunkAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: SHOW_USERDATA, payload: isShown });
    },
    clear: (): AppThunkAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: CLEAR_USERDATA });
    },
    update: (userData: IAuthenticateUserResultDto): AppThunkAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: UPDATE_USERDATA, payload: userData});
    }
}
