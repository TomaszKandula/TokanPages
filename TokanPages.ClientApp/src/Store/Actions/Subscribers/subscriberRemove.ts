import axios from "axios";
import { IAppThunkAction } from "../../Configuration";
import { IRemoveSubscriberDto } from "../../../Api/Models";
import { API_COMMAND_REMOVE_SUBSCRIBER, NULL_RESPONSE_ERROR } from "../../../Shared/constants";
import { GetTextStatusCode } from "../../../Shared/Services/Utilities";
import { RaiseError } from "../../../Shared/Services/ErrorServices";
import { EnrichConfiguration } from "../../../Api/Request";

export const REMOVE_SUBSCRIBER = "REMOVE_SUBSCRIBER";
export const REMOVE_SUBSCRIBER_RESPONSE = "REMOVE_SUBSCRIBER_RESPONSE";
export interface IRemoveSubscriber { type: typeof REMOVE_SUBSCRIBER }
export interface IRemoveSubscriberResponse { type: typeof REMOVE_SUBSCRIBER_RESPONSE }
export type TKnownActions = IRemoveSubscriber | IRemoveSubscriberResponse;

export const SubscriberRemoveAction = 
{
    remove: (payload: IRemoveSubscriberDto):  IAppThunkAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: REMOVE_SUBSCRIBER });

        axios(EnrichConfiguration(
        { 
            method: "POST", 
            url: API_COMMAND_REMOVE_SUBSCRIBER, 
            data: { id: payload.id }
        }))
        .then(response =>
        {
            if (response.status === 200)
            {
                return response.data === null 
                    ? RaiseError({ dispatch: dispatch, errorObject: NULL_RESPONSE_ERROR}) 
                    : dispatch({ type: REMOVE_SUBSCRIBER_RESPONSE });
            }
            
            RaiseError({ dispatch: dispatch, errorObject: GetTextStatusCode({ statusCode: response.status }) });
        })
        .catch(error =>
        {
            RaiseError({ dispatch: dispatch, errorObject: error });
        });
    }
}