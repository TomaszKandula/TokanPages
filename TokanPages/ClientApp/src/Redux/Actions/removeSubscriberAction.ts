import axios from "axios";
import { AppThunkAction } from "../applicationState";
import { IRemoveSubscriberDto } from "../../Api/Models";
import { API_COMMAND_REMOVE_SUBSCRIBER } from "../../Shared/constants";

export const API_REMOVE_SUBSCRIBER = "API_REMOVE_SUBSCRIBER";
export const API_REMOVE_SUBSCRIBER_RESPONSE = "API_REMOVE_SUBSCRIBER_RESPONSE";
export const REMOVE_SUBSCRIBER_ERROR = "REMOVE_SUBSCRIBER_ERROR";

export interface IApiRemoveSubscriber { type: typeof API_REMOVE_SUBSCRIBER }
export interface IApiRemoveSubscriberResponse { type: typeof API_REMOVE_SUBSCRIBER_RESPONSE, hasRemovedSubscriber: boolean }
export interface IRemoveSubscriberError { type: typeof REMOVE_SUBSCRIBER_ERROR, errorObject: any }

export type TKnownActions = 
    IApiRemoveSubscriber | 
    IApiRemoveSubscriberResponse | 
    IRemoveSubscriberError
;

export const ActionCreators = 
{
    removeSubscriber: (payload: IRemoveSubscriberDto):  AppThunkAction<TKnownActions> => async (dispatch) => 
    {
        dispatch({ type: API_REMOVE_SUBSCRIBER });

        await axios(
        { 
            method: "POST", 
            url: API_COMMAND_REMOVE_SUBSCRIBER, 
            data: 
            { 
                id: payload.id
            }
        })
        .then(response =>
        {
            return response.status === 200
                ? dispatch({ type: API_REMOVE_SUBSCRIBER_RESPONSE, hasRemovedSubscriber: true })
                : dispatch({ type: REMOVE_SUBSCRIBER_ERROR, errorObject: "Unexpected status code" });//TODO: add error object

        })
        .catch(error =>
        {
            dispatch({ type: REMOVE_SUBSCRIBER_ERROR, errorObject: error });
        });     
    }
}
