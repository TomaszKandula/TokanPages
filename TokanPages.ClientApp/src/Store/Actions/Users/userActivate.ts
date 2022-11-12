import axios from "axios";
import { IApplicationAction } from "../../Configuration";
import { IActivateUserDto } from "../../../Api/Models";
import { NULL_RESPONSE_ERROR } from "../../../Shared/constants";
import { GetTextStatusCode } from "../../../Shared/Services/Utilities";
import { RaiseError } from "../../../Shared/Services/ErrorServices";
import { EnrichConfiguration, ACTIVATE_USER } from "../../../Api/Request";

export const ACTIVATE = "ACTIVATE_ACCOUNT";
export const CLEAR = "ACTIVATE_ACCOUNT_CLEAR";
export const RESPONSE = "ACTIVATE_ACCOUNT_RESPONSE";
interface IActivate { type: typeof ACTIVATE }
interface IClear { type: typeof CLEAR }
interface IResponse { type: typeof RESPONSE; payload: any; }
export type TKnownActions = IActivate | IClear | IResponse;

export const UserActivateAction = 
{
    clear: (): IApplicationAction<TKnownActions> => (dispatch) =>
    {
        dispatch({ type: CLEAR });
    },
    activate: (payload: IActivateUserDto): IApplicationAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: ACTIVATE });

        axios(EnrichConfiguration(
        { 
            method: "POST", 
            url: ACTIVATE_USER, 
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
            
            RaiseError({ dispatch: dispatch, errorObject: GetTextStatusCode({ statusCode: response.status }) });
        })
        .catch(error => 
        {
            RaiseError({ dispatch: dispatch, errorObject: error });
        });
    }
}
