import axios from "axios";
import { AppThunkAction } from "../../applicationState";
import { IAuthenticateUserDto } from "../../../Api/Models";
import { API_COMMAND_AUTHENTICATE, NULL_RESPONSE_ERROR } from "../../../Shared/constants";
import { UnexpectedStatusCode } from "../../../Shared/textWrappers";
import { UPDATE_USERDATA, TKnownActions as TUpdateActions } from "./updateUserDataAction";
import { RaiseError } from "../../../Shared/helpers";
import { TErrorActions } from "./../raiseErrorAction";

export const SIGNIN_USER = "SIGNIN_USER";
export const SIGNIN_USER_CLEAR = "SIGNIN_USER_CLEAR";
export const SIGNIN_USER_RESPONSE = "SIGNIN_USER_RESPONSE";
export interface IApiSigninUser { type: typeof SIGNIN_USER }
export interface IApiSigninUserClear { type: typeof SIGNIN_USER_CLEAR }
export interface IApiSigninUserResponse { type: typeof SIGNIN_USER_RESPONSE }
export type TKnownActions = IApiSigninUser | IApiSigninUserClear | IApiSigninUserResponse | TErrorActions | TUpdateActions;

export const ActionCreators = 
{
    clearSignedUser: (): AppThunkAction<TKnownActions> => (dispatch) =>
    {
        dispatch({ type: SIGNIN_USER_CLEAR });
    },
    signinUser: (payload: IAuthenticateUserDto): AppThunkAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: SIGNIN_USER });

        axios(
        { 
            method: "POST", 
            url: API_COMMAND_AUTHENTICATE, 
            data: 
            {  
                emailAddress: payload.emailAddress,
                password: payload.password
            }
        })
        .then(response => 
        {
            if (response.status === 200)
            {
                const pushData = () => 
                {
                    dispatch({ type: SIGNIN_USER_RESPONSE });
                    dispatch({ type: UPDATE_USERDATA, payload: response.data });
                }
                
                return response.data === null 
                    ? RaiseError(dispatch, NULL_RESPONSE_ERROR) 
                    : pushData();
            }
            
            RaiseError(dispatch, UnexpectedStatusCode(response.status));
        })
        .catch(error => 
        {
            RaiseError(dispatch, error);
        });
    }
}
