import axios from "axios";
import { IApplicationAction } from "../../Configuration";
import { IUpdateSubscriberDto } from "../../../Api/Models";
import { NULL_RESPONSE_ERROR } from "../../../Shared/constants";
import { GetTextStatusCode } from "../../../Shared/Services/Utilities";
import { RaiseError } from "../../../Shared/Services/ErrorServices";
import { EnrichConfiguration, UPDATE_SUBSCRIBER } from "../../../Api/Request";

export const UPDATE = "UPDATE_SUBSCRIBER";
export const RESPONSE = "UPDATE_SUBSCRIBER_RESPONSE";
interface IUpdate { type: typeof UPDATE }
interface IResponse { type: typeof RESPONSE; payload: any; }
export type TKnownActions = IUpdate | IResponse;

export const SubscriberUpdateAction = 
{
    update: (payload: IUpdateSubscriberDto): IApplicationAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: UPDATE });

        axios(EnrichConfiguration(
        { 
            method: "POST", 
            url: UPDATE_SUBSCRIBER, 
            data: { email: payload.email }
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