import { IAuthenticateUserResultDto } from "../../../Api/Models";
import { AppThunkAction } from "../../applicationState";

export const CLEAR_USERDATA = "CLEAR_USERDATA";
export const UPDATE_USERDATA = "UPDATE_USERDATA";
export interface IClearUserData { type: typeof CLEAR_USERDATA }
export interface IUpdateUserData { type: typeof UPDATE_USERDATA, payload: IAuthenticateUserResultDto }
export type TKnownActions = IClearUserData | IUpdateUserData;

export const ActionCreators = 
{
    clearUserData: (): AppThunkAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: CLEAR_USERDATA });
    },
    updateUserData: (userData: IAuthenticateUserResultDto): AppThunkAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: UPDATE_USERDATA, payload: userData});
    }
}
