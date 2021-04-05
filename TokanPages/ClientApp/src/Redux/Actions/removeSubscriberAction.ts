import axios from "axios";
import { AppThunkAction } from "../applicationState";
import { IRemoveSubscriberDto } from "../../Api/Models";
import { API_COMMAND_REMOVE_SUBSCRIBER } from "../../Shared/constants";
import { GetUnexpectedStatusCode } from "../../Shared/messageHelper";

export const REMOVE_SUBSCRIBER = "REMOVE_SUBSCRIBER";
export const REMOVE_SUBSCRIBER_RESPONSE = "REMOVE_SUBSCRIBER_RESPONSE";
export const REMOVE_SUBSCRIBER_ERROR = "REMOVE_SUBSCRIBER_ERROR";

export interface IApiRemoveSubscriber { type: typeof REMOVE_SUBSCRIBER }
export interface IApiRemoveSubscriberResponse { type: typeof REMOVE_SUBSCRIBER_RESPONSE, hasRemovedSubscriber: boolean }
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
        dispatch({ type: REMOVE_SUBSCRIBER });

        await axios(
        { 
            method: "POST", 
            url: API_COMMAND_REMOVE_SUBSCRIBER, 
            data: { id: payload.id }
        })
        .then(response =>
        {
            return response.status === 200
                ? dispatch({ type: REMOVE_SUBSCRIBER_RESPONSE, hasRemovedSubscriber: true })
                : dispatch({ type: REMOVE_SUBSCRIBER_ERROR, errorObject: GetUnexpectedStatusCode(response.status) });

        })
        .catch(error =>
        {
            dispatch({ type: REMOVE_SUBSCRIBER_ERROR, errorObject: error });
        });     
    }
}
