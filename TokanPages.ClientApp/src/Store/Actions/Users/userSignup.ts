import axios from "axios";
import { IApplicationAction } from "../../Configuration";
import { IAddUserDto } from "../../../Api/Models";
import { NULL_RESPONSE_ERROR } from "../../../Shared/constants";
import { GetTextStatusCode } from "../../../Shared/Services/Utilities";
import { RaiseError } from "../../../Shared/Services/ErrorServices";
import { EnrichConfiguration, ADD_USER } from "../../../Api/Request";

export const SIGNUP = "SIGNUP_USER";
export const CLEAR = "SIGNUP_USER_CLEAR";
export const RESPONSE = "SIGNUP_USER_RESPONSE";
interface ISignup { type: typeof SIGNUP }
interface IClear { type: typeof CLEAR }
interface IResponse { type: typeof RESPONSE; payload: any; }
export type TKnownActions = ISignup | IClear | IResponse;

export const UserSignupAction = 
{
    clear: (): IApplicationAction<TKnownActions> => (dispatch) =>
    {
        dispatch({ type: CLEAR });
    },
    signup: (payload: IAddUserDto): IApplicationAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: SIGNUP });

        axios(EnrichConfiguration(
        { 
            method: "POST", 
            url: ADD_USER, 
            data: payload
        }))
        .then(response => 
        {
            if (response.status === 200)
            {
                return response.data === null 
                    ? RaiseError({dispatch: dispatch, errorObject: NULL_RESPONSE_ERROR}) 
                    : dispatch({ type: RESPONSE, payload: response.data });
            }

            RaiseError({ dispatch: dispatch, errorObject: GetTextStatusCode({ statusCode: response.status})});
        })
        .catch(error => 
        {
            RaiseError({dispatch: dispatch, errorObject: error});
        });
    }
}
