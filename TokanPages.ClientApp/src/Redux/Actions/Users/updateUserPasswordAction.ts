import axios from "axios";
import { AppThunkAction } from "../../applicationState";
import { IUpdateUserPasswordDto } from "../../../Api/Models";
import { API_COMMAND_UPDATE_USER_PASSWORD, NULL_RESPONSE_ERROR } from "../../../Shared/constants";
import { UnexpectedStatusCode } from "../../../Shared/textWrappers";
import { RaiseError } from "../../../Shared/helpers";
import { TErrorActions } from "./../raiseErrorAction";
import { EnrichConfiguration } from "../../../Api/Request";

export const UPDATE_USER_PASSWORD = "UPDATE_USER_PASSWORD";
export const UPDATE_USER_PASSWORD_CLEAR = "UPDATE_USER_PASSWORD_CLEAR";
export const UPDATE_USER_PASSWORD_RESPONSE = "UPDATE_USER_PASSWORD_RESPONSE";
export interface IApiUpdateUserPassword { type: typeof UPDATE_USER_PASSWORD }
export interface IApiUpdateUserPasswordClear { type: typeof UPDATE_USER_PASSWORD_CLEAR }
export interface IApiUpdateUserPasswordResponse { type: typeof UPDATE_USER_PASSWORD_RESPONSE }
export type TKnownActions = IApiUpdateUserPassword | IApiUpdateUserPasswordClear | IApiUpdateUserPasswordResponse | TErrorActions;

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
            data: 
            {  
                userId: payload.Id,
                newPassword: payload.NewPassword,
                resetId: payload.ResetId
            }
        }))
        .then(response => 
        {
            if (response.status === 200)
            {
                return response.data === null 
                    ? RaiseError(dispatch, NULL_RESPONSE_ERROR) 
                    : dispatch({ type: UPDATE_USER_PASSWORD_RESPONSE });
            }
            
            RaiseError(dispatch, UnexpectedStatusCode(response.status));
        })
        .catch(error => 
        {
            RaiseError(dispatch, error);
        });
    }
}
