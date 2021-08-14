import axios from "axios";
import { AppThunkAction } from "../applicationState";
import { IAuthenticateUserDto, IAuthenticateUserResultDto } from "../../Api/Models";
import { API_COMMAND_AUTHENTICATE, NULL_RESPONSE_ERROR } from "../../Shared/constants";
import { UnexpectedStatusCode } from "../../Shared/textWrappers";
import { RaiseError } from "../../Shared/helpers";
import { TErrorActions } from "./raiseErrorAction";

export const SIGNIN_USER = "SIGNIN_USER";
export const SIGNIN_USER_RESPONSE = "SIGNIN_USER_RESPONSE";
export interface IApiSigninUser { type: typeof SIGNIN_USER }
export interface IApiSigninUserResponse { type: typeof SIGNIN_USER_RESPONSE, payload: IAuthenticateUserResultDto }
export type TKnownActions = IApiSigninUser | IApiSigninUserResponse | TErrorActions;

export const ActionCreators = 
{
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
                return response.data === null 
                    ? RaiseError(dispatch, NULL_RESPONSE_ERROR) 
                    : dispatch({ type: SIGNIN_USER_RESPONSE, payload: response.data });
            }
            
            RaiseError(dispatch, UnexpectedStatusCode(response.status));
        })
        .catch(error => 
        {
            RaiseError(dispatch, error);
        });
    }
}
