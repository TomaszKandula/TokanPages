import axios from "axios";
import { AppThunkAction } from "../../Configuration";
import { IRemoveUserDto } from "../../../Api/Models";
import { API_COMMAND_REMOVE_USER, NULL_RESPONSE_ERROR } from "../../../Shared/constants";
import { GetTextStatusCode } from "../../../Shared/Services/Utilities";
import { RaiseError } from "../../../Shared/Services/ErrorServices";
import { TErrorActions } from "../raiseErrorAction";
import { EnrichConfiguration } from "../../../Api/Request";

export const REMOVE_ACCOUNT = "REMOVE_ACCOUNT";
export const REMOVE_ACCOUNT_CLEAR = "REMOVE_ACCOUNT_CLEAR";
export const REMOVE_ACCOUNT_RESPONSE = "REMOVE_ACCOUNT_RESPONSE";
export interface IApiRemoveAccount { type: typeof REMOVE_ACCOUNT }
export interface IApiRemoveAccountClear { type: typeof REMOVE_ACCOUNT_CLEAR }
export interface IApiRemoveAccountResponse { type: typeof REMOVE_ACCOUNT_RESPONSE }
export type TKnownActions = IApiRemoveAccount | IApiRemoveAccountClear | IApiRemoveAccountResponse | TErrorActions;

export const ActionCreators = 
{
    clear: (): AppThunkAction<TKnownActions> => (dispatch) =>
    {
        dispatch({ type: REMOVE_ACCOUNT_CLEAR });
    },
    removeAccount: (payload: IRemoveUserDto): AppThunkAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: REMOVE_ACCOUNT });

        axios(EnrichConfiguration(
        { 
            method: "POST", 
            url: API_COMMAND_REMOVE_USER, 
            data: payload
        }))
        .then(response => 
        {
            if (response.status === 200)
            {
                return response.data === null 
                    ? RaiseError({ dispatch: dispatch, errorObject: NULL_RESPONSE_ERROR }) 
                    : dispatch({ type: REMOVE_ACCOUNT_RESPONSE });
            }
            
            RaiseError({ dispatch: dispatch, errorObject: GetTextStatusCode({ statusCode: response.status }) });
        })
        .catch(error => 
        {
            RaiseError({ dispatch: dispatch, errorObject: error });
        });
    }
}
