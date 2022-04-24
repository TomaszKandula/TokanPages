import axios from "axios";
import { AppThunkAction } from "../../applicationState";
import { IUpdateUserDto } from "../../../Api/Models";
import { API_COMMAND_UPDATE_USER, NULL_RESPONSE_ERROR } from "../../../Shared/constants";
import { GetTextStatusCode } from "../../../Shared/Services/Utilities";
import { RaiseError } from "../../../Shared/Services/ErrorServices";
import { TErrorActions } from "./../raiseErrorAction";
import { EnrichConfiguration } from "../../../Api/Request";

export const UPDATE_USER = "UPDATE_USER";
export const UPDATE_USER_CLEAR = "UPDATE_USER_CLEAR";
export const UPDATE_USER_RESPONSE = "UPDATE_USER_RESPONSE";
export interface IApiUpdateUser { type: typeof UPDATE_USER }
export interface IApiUpdateUserClear { type: typeof UPDATE_USER_CLEAR }
export interface IApiUpdateUserResponse { type: typeof UPDATE_USER_RESPONSE }
export type TKnownActions = IApiUpdateUser | IApiUpdateUserClear | IApiUpdateUserResponse | TErrorActions;

export const ActionCreators = 
{
    clear: (): AppThunkAction<TKnownActions> => (dispatch) =>
    {
        dispatch({ type: UPDATE_USER_CLEAR });
    },
    update: (payload: IUpdateUserDto): AppThunkAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: UPDATE_USER });

        axios(EnrichConfiguration(
        { 
            method: "POST", 
            url: API_COMMAND_UPDATE_USER, 
            data: payload
        }))
        .then(response => 
        {
            if (response.status === 200)
            {
                return response.data === null 
                    ? RaiseError({ dispatch: dispatch, errorObject: NULL_RESPONSE_ERROR}) 
                    : dispatch({ type: UPDATE_USER_RESPONSE });
            }
            
            RaiseError({ dispatch: dispatch, errorObject: GetTextStatusCode({ statusCode: response.status })});
        })
        .catch(error => 
        {
            RaiseError({ dispatch: dispatch, errorObject: error });
        });
    }
}
