import axios from "axios";
import { IApplicationAction } from "../../Configuration";
import { IUpdateUserPasswordDto } from "../../../Api/Models";
import { NULL_RESPONSE_ERROR } from "../../../Shared/constants";
import { GetTextStatusCode } from "../../../Shared/Services/Utilities";
import { RaiseError } from "../../../Shared/Services/ErrorServices";
import { EnrichConfiguration, UPDATE_USER_PASSWORD } from "../../../Api/Request";

export const UPDATE = "UPDATE_USER_PASSWORD";
export const CLEAR = "UPDATE_USER_PASSWORD_CLEAR";
export const RESPONSE = "UPDATE_USER_PASSWORD_RESPONSE";
interface IUpdate { type: typeof UPDATE }
interface IClear { type: typeof CLEAR }
interface IResponse { type: typeof RESPONSE; payload: any; }
export type TKnownActions = IUpdate | IClear | IResponse;

export const UserPasswordUpdateAction = 
{
    clear: (): IApplicationAction<TKnownActions> => (dispatch) =>
    {
        dispatch({ type: CLEAR });
    },
    update: (payload: IUpdateUserPasswordDto): IApplicationAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: UPDATE });

        axios(EnrichConfiguration(
        { 
            method: "POST", 
            url: UPDATE_USER_PASSWORD, 
            data: payload
        }))
        .then(response => 
        {
            if (response.status === 200)
            {
                return response.data === null 
                    ? RaiseError({ dispatch: dispatch, errorObject: NULL_RESPONSE_ERROR}) 
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
