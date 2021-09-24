import axios from "axios";
import { AppThunkAction } from "../../applicationState";
import { IActivateUserDto } from "../../../Api/Models";
import { API_COMMAND_ACTIVATE_USER, NULL_RESPONSE_ERROR } from "../../../Shared/constants";
import { UnexpectedStatusCode } from "../../../Shared/textWrappers";
import { TKnownActions as TUpdateActions } from "./updateUserDataAction";
import { RaiseError } from "../../../Shared/helpers";
import { TErrorActions } from "./../raiseErrorAction";

export const ACTIVATE_ACCOUNT = "ACTIVATE_ACCOUNT";
export const ACTIVATE_ACCOUNT_CLEAR = "ACTIVATE_ACCOUNT_CLEAR";
export const ACTIVATE_ACCOUNT_RESPONSE = "ACTIVATE_ACCOUNT_RESPONSE";
export interface IApiActivateAccount { type: typeof ACTIVATE_ACCOUNT }
export interface IApiActivateAccountClear { type: typeof ACTIVATE_ACCOUNT_CLEAR }
export interface IApiActivateAccountResponse { type: typeof ACTIVATE_ACCOUNT_RESPONSE }
export type TKnownActions = IApiActivateAccount | IApiActivateAccountClear | IApiActivateAccountResponse | TErrorActions | TUpdateActions;

export const ActionCreators = 
{
    clear: (): AppThunkAction<TKnownActions> => (dispatch) =>
    {
        dispatch({ type: ACTIVATE_ACCOUNT_CLEAR });
    },
    activateAccount: (payload: IActivateUserDto): AppThunkAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: ACTIVATE_ACCOUNT });

        axios(
        { 
            method: "POST", 
            url: API_COMMAND_ACTIVATE_USER, 
            data: 
            {  
                activationId: payload.activationId
            }
        })
        .then(response => 
        {
            if (response.status === 200)
            {
                return response.data === null 
                    ? RaiseError(dispatch, NULL_RESPONSE_ERROR) 
                    : dispatch({ type: ACTIVATE_ACCOUNT_RESPONSE });
            }
            
            RaiseError(dispatch, UnexpectedStatusCode(response.status));
        })
        .catch(error => 
        {
            RaiseError(dispatch, error);
        });
    }
}
