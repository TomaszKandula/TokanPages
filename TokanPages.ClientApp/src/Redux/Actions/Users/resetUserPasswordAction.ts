import axios from "axios";
import { AppThunkAction } from "../../applicationState";
import { IResetUserPasswordDto } from "../../../Api/Models";
import { API_COMMAND_RESET_USER_PASSWORD, NULL_RESPONSE_ERROR } from "../../../Shared/constants";
import { TKnownActions as TUpdateActions } from "./storeUserDataAction";
import { GetTextStatusCode } from "../../../Shared/Services/Utilities";
import { RaiseError } from "../../../Shared/Services/ErrorServices";
import { TErrorActions } from "./../raiseErrorAction";
import { EnrichConfiguration } from "../../../Api/Request";

export const RESET_USER_PASSWORD = "RESET_USER_PASSWORD";
export const RESET_USER_PASSWORD_CLEAR = "RESET_USER_PASSWORD_CLEAR";
export const RESET_USER_PASSWORD_RESPONSE = "RESET_USER_PASSWORD_RESPONSE";
export interface IApiResetUserPassword { type: typeof RESET_USER_PASSWORD }
export interface IApiResetUserPasswordClear { type: typeof RESET_USER_PASSWORD_CLEAR }
export interface IApiResetUserPasswordResponse { type: typeof RESET_USER_PASSWORD_RESPONSE }
export type TKnownActions = IApiResetUserPassword | IApiResetUserPasswordClear | IApiResetUserPasswordResponse | TErrorActions | TUpdateActions;

export const ActionCreators = 
{
    clear: (): AppThunkAction<TKnownActions> => (dispatch) =>
    {
        dispatch({ type: RESET_USER_PASSWORD_CLEAR });
    },
    reset: (payload: IResetUserPasswordDto): AppThunkAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: RESET_USER_PASSWORD });

        axios(EnrichConfiguration(
        { 
            method: "POST", 
            url: API_COMMAND_RESET_USER_PASSWORD, 
            data: 
            {  
                emailAddress: payload.emailAddress
            }
        }))
        .then(response => 
        {
            if (response.status === 200)
            {
                return response.data === null 
                    ? RaiseError({ dispatch: dispatch, errorObject: NULL_RESPONSE_ERROR }) 
                    : dispatch({ type: RESET_USER_PASSWORD_RESPONSE });
            }
            
            RaiseError({ dispatch: dispatch, errorObject: GetTextStatusCode({ statusCode: response.status })});
        })
        .catch(error => 
        {
            RaiseError({ dispatch: dispatch, errorObject: error });
        });
    }
}
