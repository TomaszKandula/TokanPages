import axios from "axios";
import { IApplicationAction } from "../../Configuration";
import { IActivateUserDto } from "../../../Api/Models";
import { API_COMMAND_ACTIVATE_USER, NULL_RESPONSE_ERROR } from "../../../Shared/constants";
import { GetTextStatusCode } from "../../../Shared/Services/Utilities";
import { RaiseError } from "../../../Shared/Services/ErrorServices";
import { EnrichConfiguration } from "../../../Api/Request";

export const ACTIVATE_ACCOUNT = "ACTIVATE_ACCOUNT";
export const ACTIVATE_ACCOUNT_CLEAR = "ACTIVATE_ACCOUNT_CLEAR";
export const ACTIVATE_ACCOUNT_RESPONSE = "ACTIVATE_ACCOUNT_RESPONSE";
export interface IActivateAccount { type: typeof ACTIVATE_ACCOUNT }
export interface IActivateAccountClear { type: typeof ACTIVATE_ACCOUNT_CLEAR }
export interface IActivateAccountResponse { type: typeof ACTIVATE_ACCOUNT_RESPONSE }
export type TKnownActions = IActivateAccount | IActivateAccountClear | IActivateAccountResponse;

export const UserActivateAction = 
{
    clear: (): IApplicationAction<TKnownActions> => (dispatch) =>
    {
        dispatch({ type: ACTIVATE_ACCOUNT_CLEAR });
    },
    activate: (payload: IActivateUserDto): IApplicationAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: ACTIVATE_ACCOUNT });

        axios(EnrichConfiguration(
        { 
            method: "POST", 
            url: API_COMMAND_ACTIVATE_USER, 
            data: 
            {  
                activationId: payload.activationId
            }
        }))
        .then(response => 
        {
            if (response.status === 200)
            {
                return response.data === null 
                    ? RaiseError({ dispatch: dispatch, errorObject: NULL_RESPONSE_ERROR }) 
                    : dispatch({ type: ACTIVATE_ACCOUNT_RESPONSE });
            }
            
            RaiseError({ dispatch: dispatch, errorObject: GetTextStatusCode({ statusCode: response.status }) });
        })
        .catch(error => 
        {
            RaiseError({ dispatch: dispatch, errorObject: error });
        });
    }
}