import axios from "axios";
import { AppThunkAction } from "../../Configuration";
import { IAuthenticateUserDto } from "../../../Api/Models";
import { API_COMMAND_AUTHENTICATE, NULL_RESPONSE_ERROR } from "../../../Shared/constants";
import { UPDATE_USERDATA, TKnownActions as TUpdateActions } from "./storeUserDataAction";
import { GetTextStatusCode } from "../../../Shared/Services/Utilities";
import { RaiseError } from "../../../Shared/Services/ErrorServices";
import { TErrorActions } from "../raiseErrorAction";
import { EnrichConfiguration } from "../../../Api/Request";

export const SIGNIN_USER = "SIGNIN_USER";
export const SIGNIN_USER_CLEAR = "SIGNIN_USER_CLEAR";
export const SIGNIN_USER_RESPONSE = "SIGNIN_USER_RESPONSE";
export interface IApiSigninUser { type: typeof SIGNIN_USER }
export interface IApiSigninUserClear { type: typeof SIGNIN_USER_CLEAR }
export interface IApiSigninUserResponse { type: typeof SIGNIN_USER_RESPONSE }
export type TKnownActions = IApiSigninUser | IApiSigninUserClear | IApiSigninUserResponse | TErrorActions | TUpdateActions;

export const ActionCreators = 
{
    clear: (): AppThunkAction<TKnownActions> => (dispatch) =>
    {
        dispatch({ type: SIGNIN_USER_CLEAR });
    },
    signin: (payload: IAuthenticateUserDto): AppThunkAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: SIGNIN_USER });

        axios(EnrichConfiguration(
        { 
            method: "POST", 
            url: API_COMMAND_AUTHENTICATE, 
            data: 
            {  
                emailAddress: payload.emailAddress,
                password: payload.password
            }
        }))
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
                    ? RaiseError({ dispatch: dispatch, errorObject: NULL_RESPONSE_ERROR }) 
                    : pushData();
            }
            
            RaiseError({ dispatch: dispatch, errorObject: GetTextStatusCode({ statusCode: response.status}) });
        })
        .catch(error => 
        {
            RaiseError({ dispatch: dispatch, errorObject: error });
        });
    }
}
