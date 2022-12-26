import axios from "axios";
import { IApplicationAction } from "../../Configuration";
import { IRemoveUserDto } from "../../../Api/Models";
import { NULL_RESPONSE_ERROR } from "../../../Shared/constants";
import { GetTextStatusCode } from "../../../Shared/Services/Utilities";
import { RaiseError } from "../../../Shared/Services/ErrorServices";
import { EnrichConfiguration, REMOVE_USER } from "../../../Api/Request";

export const REMOVE = "REMOVE_ACCOUNT";
export const CLEAR = "REMOVE_ACCOUNT_CLEAR";
export const RESPONSE = "REMOVE_ACCOUNT_RESPONSE";
interface IRemove { type: typeof REMOVE }
interface IClear { type: typeof CLEAR }
interface IResponse { type: typeof RESPONSE; payload: any; }
export type TKnownActions = IRemove | IClear | IResponse;

export const UserRemoveAction = 
{
    clear: (): IApplicationAction<TKnownActions> => (dispatch) =>
    {
        dispatch({ type: CLEAR });
    },
    remove: (payload: IRemoveUserDto): IApplicationAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: REMOVE });

        axios(EnrichConfiguration(
        { 
            method: "POST", 
            url: REMOVE_USER, 
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
