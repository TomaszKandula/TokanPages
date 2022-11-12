import axios from "axios";
import { IApplicationAction } from "../../Configuration";
import { IRemoveSubscriberDto } from "../../../Api/Models";
import { NULL_RESPONSE_ERROR } from "../../../Shared/constants";
import { GetTextStatusCode } from "../../../Shared/Services/Utilities";
import { RaiseError } from "../../../Shared/Services/ErrorServices";
import { EnrichConfiguration, REMOVE_SUBSCRIBER } from "../../../Api/Request";

export const REMOVE = "REMOVE_SUBSCRIBER";
export const RESPONSE = "REMOVE_SUBSCRIBER_RESPONSE";
interface IRemove { type: typeof REMOVE }
interface IResponse { type: typeof RESPONSE; payload: any; }
export type TKnownActions = IRemove | IResponse;

export const SubscriberRemoveAction = 
{
    remove: (payload: IRemoveSubscriberDto):  IApplicationAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: REMOVE });

        axios(EnrichConfiguration(
        { 
            method: "POST", 
            url: REMOVE_SUBSCRIBER, 
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
            
            RaiseError({ dispatch: dispatch, errorObject: GetTextStatusCode({ statusCode: response.status }) });
        })
        .catch(error =>
        {
            RaiseError({ dispatch: dispatch, errorObject: error });
        });
    }
}