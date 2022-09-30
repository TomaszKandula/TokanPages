import axios from "axios";
import { AppThunkAction } from "../../Configuration";
import { IUpdateSubscriberDto } from "../../../Api/Models";
import { API_COMMAND_UPDATE_SUBSCRIBER, NULL_RESPONSE_ERROR } from "../../../Shared/constants";
import { GetTextStatusCode } from "../../../Shared/Services/Utilities";
import { RaiseError } from "../../../Shared/Services/ErrorServices";
import { TErrorActions } from "../raiseErrorAction";
import { EnrichConfiguration } from "../../../Api/Request";

export const UPDATE_SUBSCRIBER = "UPDATE_SUBSCRIBER";
export const UPDATE_SUBSCRIBER_RESPONSE = "UPDATE_SUBSCRIBER_RESPONSE";
export interface IApiUpdateSubscriber { type: typeof UPDATE_SUBSCRIBER }
export interface IApiUpdateSubscriberResponse { type: typeof UPDATE_SUBSCRIBER_RESPONSE }
export type TKnownActions = IApiUpdateSubscriber | IApiUpdateSubscriberResponse | TErrorActions;

export const ActionCreators = 
{
    update: (payload: IUpdateSubscriberDto): AppThunkAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: UPDATE_SUBSCRIBER });

        axios(EnrichConfiguration(
        { 
            method: "POST", 
            url: API_COMMAND_UPDATE_SUBSCRIBER, 
            data: { email: payload.email }
        }))
        .then(response => 
        {
            if (response.status === 200)
            {
                return response.data === null 
                    ? RaiseError({ dispatch: dispatch, errorObject: NULL_RESPONSE_ERROR }) 
                    : dispatch({ type: UPDATE_SUBSCRIBER_RESPONSE });
            }

            RaiseError({ dispatch: dispatch, errorObject: GetTextStatusCode({ statusCode: response.status }) });
        })
        .catch(error => 
        {
            RaiseError({ dispatch: dispatch, errorObject: error });
        });     
    }
}