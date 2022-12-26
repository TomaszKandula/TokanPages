import axios from "axios";
import { IApplicationAction } from "../../Configuration";
import { IResetUserPasswordDto } from "../../../Api/Models";
import { NULL_RESPONSE_ERROR } from "../../../Shared/constants";
import { GetTextStatusCode } from "../../../Shared/Services/Utilities";
import { RaiseError } from "../../../Shared/Services/ErrorServices";
import { EnrichConfiguration, RESET_USER_PASSWORD } from "../../../Api/Request";

export const RESET = "RESET_USER_PASSWORD";
export const CLEAR = "RESET_USER_PASSWORD_CLEAR";
export const RESPONSE = "RESET_USER_PASSWORD_RESPONSE";
interface IReset { type: typeof RESET }
interface IClear { type: typeof CLEAR }
interface IResponse { type: typeof RESPONSE; payload: any; }
export type TKnownActions = IReset | IClear | IResponse;

export const UserPasswordResetAction = 
{
    clear: (): IApplicationAction<TKnownActions> => (dispatch) =>
    {
        dispatch({ type: CLEAR });
    },
    reset: (payload: IResetUserPasswordDto): IApplicationAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: RESET });

        axios(EnrichConfiguration(
        { 
            method: "POST", 
            url: RESET_USER_PASSWORD, 
            data: payload
        }))
        .then(response => 
        {
            if (response.status === 200)
            {
                return response.data === null 
                    ? RaiseError({ dispatch: dispatch, errorObject: NULL_RESPONSE_ERROR }) 
                    : dispatch({ type: RESPONSE, payload: response.data });
            }
            
            RaiseError({ dispatch: dispatch, errorObject: GetTextStatusCode({ statusCode: response.status })});
        })
        .catch(error => 
        {
            RaiseError({ dispatch: dispatch, errorObject: error });
        });
    }
}
