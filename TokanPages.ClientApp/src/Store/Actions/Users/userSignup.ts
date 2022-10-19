import axios from "axios";
import { IApplicationAction } from "../../Configuration";
import { IAddUserDto } from "../../../Api/Models";
import { API_COMMAND_ADD_USER, NULL_RESPONSE_ERROR } from "../../../Shared/constants";
import { GetTextStatusCode } from "../../../Shared/Services/Utilities";
import { RaiseError } from "../../../Shared/Services/ErrorServices";
import { EnrichConfiguration } from "../../../Api/Request";

export const SIGNUP_USER = "SIGNUP_USER";
export const SIGNUP_USER_CLEAR = "SIGNUP_USER_CLEAR";
export const SIGNUP_USER_RESPONSE = "SIGNUP_USER_RESPONSE";
export interface ISignupUser { type: typeof SIGNUP_USER }
export interface ISignupUserClear { type: typeof SIGNUP_USER_CLEAR }
export interface ISignupUserResponse { type: typeof SIGNUP_USER_RESPONSE; payload: any; }
export type TKnownActions = ISignupUser | ISignupUserClear | ISignupUserResponse;

export const UserSignupAction = 
{
    clear: (): IApplicationAction<TKnownActions> => (dispatch) =>
    {
        dispatch({ type: SIGNUP_USER_CLEAR });
    },
    signup: (payload: IAddUserDto): IApplicationAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: SIGNUP_USER });

        axios(EnrichConfiguration(
        { 
            method: "POST", 
            url: API_COMMAND_ADD_USER, 
            data: payload
        }))
        .then(response => 
        {
            if (response.status === 200)
            {
                return response.data === null 
                    ? RaiseError({dispatch: dispatch, errorObject: NULL_RESPONSE_ERROR}) 
                    : dispatch({ type: SIGNUP_USER_RESPONSE, payload: response.data });
            }

            RaiseError({ dispatch: dispatch, errorObject: GetTextStatusCode({ statusCode: response.status})});
        })
        .catch(error => 
        {
            RaiseError({dispatch: dispatch, errorObject: error});
        });
    }
}
