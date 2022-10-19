import axios from "axios";
import { IApplicationAction } from "../../Configuration";
import { IUpdateUserDto } from "../../../Api/Models";
import { API_COMMAND_UPDATE_USER, NULL_RESPONSE_ERROR } from "../../../Shared/constants";
import { GetTextStatusCode } from "../../../Shared/Services/Utilities";
import { RaiseError } from "../../../Shared/Services/ErrorServices";
import { EnrichConfiguration } from "../../../Api/Request";

export const UPDATE_USER = "UPDATE_USER";
export const UPDATE_USER_CLEAR = "UPDATE_USER_CLEAR";
export const UPDATE_USER_RESPONSE = "UPDATE_USER_RESPONSE";
export interface IUpdateUser { type: typeof UPDATE_USER }
export interface IUpdateUserClear { type: typeof UPDATE_USER_CLEAR }
export interface IUpdateUserResponse { type: typeof UPDATE_USER_RESPONSE; payload: any; }
export type TKnownActions = IUpdateUser | IUpdateUserClear | IUpdateUserResponse;

export const UserUpdateAction = 
{
    clear: (): IApplicationAction<TKnownActions> => (dispatch) =>
    {
        dispatch({ type: UPDATE_USER_CLEAR });
    },
    update: (payload: IUpdateUserDto): IApplicationAction<TKnownActions> => (dispatch) => 
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
                    : dispatch({ type: UPDATE_USER_RESPONSE, payload: response.data });
            }
            
            RaiseError({ dispatch: dispatch, errorObject: GetTextStatusCode({ statusCode: response.status })});
        })
        .catch(error => 
        {
            RaiseError({ dispatch: dispatch, errorObject: error });
        });
    }
}
