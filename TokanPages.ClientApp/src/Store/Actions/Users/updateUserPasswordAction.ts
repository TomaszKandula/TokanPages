import axios from "axios";
import { AppThunkAction } from "../../Configuration";
import { IUpdateUserPasswordDto } from "../../../Api/Models";
import { API_COMMAND_UPDATE_USER_PASSWORD, NULL_RESPONSE_ERROR } from "../../../Shared/constants";
import { GetTextStatusCode } from "../../../Shared/Services/Utilities";
import { RaiseError } from "../../../Shared/Services/ErrorServices";
import { EnrichConfiguration } from "../../../Api/Request";

export const UPDATE_USER_PASSWORD = "UPDATE_USER_PASSWORD";
export const UPDATE_USER_PASSWORD_CLEAR = "UPDATE_USER_PASSWORD_CLEAR";
export const UPDATE_USER_PASSWORD_RESPONSE = "UPDATE_USER_PASSWORD_RESPONSE";
export interface IUpdateUserPassword { type: typeof UPDATE_USER_PASSWORD }
export interface IUpdateUserPasswordClear { type: typeof UPDATE_USER_PASSWORD_CLEAR }
export interface IUpdateUserPasswordResponse { type: typeof UPDATE_USER_PASSWORD_RESPONSE }
export type TKnownActions = IUpdateUserPassword | IUpdateUserPasswordClear | IUpdateUserPasswordResponse;

export const ActionCreators = 
{
    clear: (): AppThunkAction<TKnownActions> => (dispatch) =>
    {
        dispatch({ type: UPDATE_USER_PASSWORD_CLEAR });
    },
    update: (payload: IUpdateUserPasswordDto): AppThunkAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: UPDATE_USER_PASSWORD });

        axios(EnrichConfiguration(
        { 
            method: "POST", 
            url: API_COMMAND_UPDATE_USER_PASSWORD, 
            data: payload
        }))
        .then(response => 
        {
            if (response.status === 200)
            {
                return response.data === null 
                    ? RaiseError({ dispatch: dispatch, errorObject: NULL_RESPONSE_ERROR}) 
                    : dispatch({ type: UPDATE_USER_PASSWORD_RESPONSE });
            }
            
            RaiseError({ dispatch: dispatch, errorObject: GetTextStatusCode({ statusCode: response.status })});
        })
        .catch(error => 
        {
            RaiseError({ dispatch: dispatch, errorObject: error });
        });
    }
}
