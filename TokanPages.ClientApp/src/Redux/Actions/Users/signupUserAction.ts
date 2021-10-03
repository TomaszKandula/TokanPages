import axios from "axios";
import { AppThunkAction } from "../../applicationState";
import { IAddUserDto } from "../../../Api/Models";
import { API_COMMAND_ADD_USER, NULL_RESPONSE_ERROR } from "../../../Shared/constants";
import { UnexpectedStatusCode } from "../../../Shared/textWrappers";
import { RaiseError } from "../../../Shared/helpers";
import { TErrorActions } from "./../raiseErrorAction";
import { EnrichConfiguration } from "../../../Api/Request";

export const SIGNUP_USER = "SIGNUP_USER";
export const SIGNUP_USER_CLEAR = "SIGNUP_USER_CLEAR";
export const SIGNUP_USER_RESPONSE = "SIGNUP_USER_RESPONSE";
export interface IApiSignupUser { type: typeof SIGNUP_USER }
export interface IApiSignupUserClear { type: typeof SIGNUP_USER_CLEAR }
export interface IApiSignupUserResponse { type: typeof SIGNUP_USER_RESPONSE }
export type TKnownActions = IApiSignupUser | IApiSignupUserClear | IApiSignupUserResponse | TErrorActions;

export const ActionCreators = 
{
    clear: (): AppThunkAction<TKnownActions> => (dispatch) =>
    {
        dispatch({ type: SIGNUP_USER_CLEAR });
    },
    signup: (payload: IAddUserDto): AppThunkAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: SIGNUP_USER });

        axios(EnrichConfiguration(
        { 
            method: "POST", 
            url: API_COMMAND_ADD_USER, 
            data: 
            {  
                userAlias: payload.userAlias,
                firstName: payload.firstName,
                lastName: payload.lastName,
                emailAddress: payload.emailAddress,
                password: payload.password
            }
        }))
        .then(response => 
        {
            if (response.status === 200)
            {
                return response.data === null 
                    ? RaiseError(dispatch, NULL_RESPONSE_ERROR) 
                    : dispatch({ type: SIGNUP_USER_RESPONSE });
            }
            
            RaiseError(dispatch, UnexpectedStatusCode(response.status));
        })
        .catch(error => 
        {
            RaiseError(dispatch, error);
        });
    }
}
