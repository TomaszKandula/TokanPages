import axios from "axios";
import { AppThunkAction } from "../applicationState";
import { IUpdateSubscriberDto } from "../../Api/Models";
import { API_COMMAND_UPDATE_SUBSCRIBER } from "../../Shared/constants";
import { UnexpectedStatusCode } from "../../Shared/textWrappers";
import { GetErrorMessage } from "../../Shared/helpers";

export const UPDATE_SUBSCRIBER = "UPDATE_SUBSCRIBER";
export const UPDATE_SUBSCRIBER_RESPONSE = "UPDATE_SUBSCRIBER_RESPONSE";
export const UPDATE_SUBSCRIBER_ERROR = "UPDATE_SUBSCRIBER_ERROR";

export interface IApiUpdateSubscriber { type: typeof UPDATE_SUBSCRIBER }
export interface IApiUpdateSubscriberResponse { type: typeof UPDATE_SUBSCRIBER_RESPONSE, hasUpdatedSubscriber: boolean }
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
        dispatch({ type: UPDATE_SUBSCRIBER });

        await axios(
        { 
            method: "POST", 
            url: API_COMMAND_UPDATE_SUBSCRIBER, 
            data: { email: payload.email }
        })
        .then(response => 
        {
            return response.status === 200
                ? dispatch({ type: UPDATE_SUBSCRIBER_RESPONSE, hasUpdatedSubscriber: true })
                : dispatch({ type: UPDATE_SUBSCRIBER_ERROR, errorObject: UnexpectedStatusCode(response.status) });
        })
        .catch(error => 
        {
            dispatch({ type: UPDATE_SUBSCRIBER_ERROR, errorObject: GetErrorMessage(error) });
        });     
    }
}
