import axios from "axios";
import { AppThunkAction } from "../applicationState";
import { IUpdateSubscriberDto } from "../../Api/Models";
import { API_COMMAND_UPDATE_SUBSCRIBER } from "../../Shared/constants";

export const API_UPDATE_SUBSCRIBER = "API_UPDATE_SUBSCRIBER";
export const API_UPDATE_SUBSCRIBER_RESPONSE = "API_UPDATE_SUBSCRIBER_RESPONSE";
export const UPDATE_SUBSCRIBER_ERROR = "UPDATE_SUBSCRIBER_ERROR";

export interface IApiUpdateSubscriber { type: typeof API_UPDATE_SUBSCRIBER }
export interface IApiUpdateSubscriberResponse { type: typeof API_UPDATE_SUBSCRIBER_RESPONSE, hasUpdatedSubscriber: boolean }
export interface IUpdateSubscriberError { type: typeof UPDATE_SUBSCRIBER_ERROR, errorObject: any }

export type TKnownActions = 
    IApiUpdateSubscriber | 
    IApiUpdateSubscriberResponse | 
    IUpdateSubscriberError
;

export const ActionCreators = 
{
    updateSubscriber: (payload: IUpdateSubscriberDto):  AppThunkAction<TKnownActions> => async (dispatch) => 
    {
        dispatch({ type: API_UPDATE_SUBSCRIBER });

        await axios(
        { 
            method: "POST", 
            url: API_COMMAND_UPDATE_SUBSCRIBER, 
            data: { email: payload.email }
        })
        .then(response => 
        {
            return response.status === 200
                ? dispatch({ type: API_UPDATE_SUBSCRIBER_RESPONSE, hasUpdatedSubscriber: true })
                : dispatch({ type: UPDATE_SUBSCRIBER_ERROR, errorObject: "Unexpected status code" });//TODO: add error object
        })
        .catch(error => 
        {
            dispatch({ type: UPDATE_SUBSCRIBER_ERROR, errorObject: error });
        });     
    }
}
