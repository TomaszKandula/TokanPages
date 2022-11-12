import { IAuthenticateUserResultDto } from "../../../Api/Models";
import { IApplicationAction } from "../../Configuration";

export const SHOW = "SHOW_USERDATA";
export const CLEAR = "CLEAR_USERDATA";
export const UPDATE = "UPDATE_USERDATA";
interface IShow { type: typeof SHOW, payload: boolean }
interface IClear { type: typeof CLEAR }
interface IUpdate { type: typeof UPDATE, payload: IAuthenticateUserResultDto }
export type TKnownActions = IShow | IClear | IUpdate;

export const UserDataStoreAction = 
{
    show: (isShown: boolean): IApplicationAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: SHOW, payload: isShown });
    },
    clear: (): IApplicationAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: CLEAR });
    },
    update: (userData: IAuthenticateUserResultDto): IApplicationAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: UPDATE, payload: userData });
    }
}
