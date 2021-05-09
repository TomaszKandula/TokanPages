import * as Sentry from "@sentry/react";
import axios from "axios";
import { AppThunkAction } from "../applicationState";
import { IAddSubscriberDto } from "../../Api/Models";
import { API_COMMAND_ADD_SUBSCRIBER } from "../../Shared/constants";
import { UnexpectedStatusCode } from "../../Shared/textWrappers";
import { GetErrorMessage } from "../../Shared/helpers";

export const ADD_SUBSCRIBER = "ADD_SUBSCRIBER";
export const ADD_SUBSCRIBER_CLEAR = "ADD_SUBSCRIBER_CLEAR";
export const ADD_SUBSCRIBER_RESPONSE = "ADD_SUBSCRIBER_RESPONSE";
export const ADD_SUBSCRIBER_ERROR = "ADD_SUBSCRIBER_ERROR";

export interface IApiAddSubscriber { type: typeof ADD_SUBSCRIBER }
export interface IApiAddSubscriberClear { type: typeof ADD_SUBSCRIBER_CLEAR }
export interface IApiAddSubscriberResponse { type: typeof ADD_SUBSCRIBER_RESPONSE, hasAddedSubscriber: boolean }
export interface IAddSubscriberError { type: typeof ADD_SUBSCRIBER_ERROR, errorObject: any }

export type TKnownActions = 
    IApiAddSubscriber | 
    IApiAddSubscriberClear | 
    IApiAddSubscriberResponse | 
    IAddSubscriberError
;

export const ActionCreators = 
{
    addSubscriberClear: (): AppThunkAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: ADD_SUBSCRIBER_CLEAR });
    },    
    addSubscriber: (payload: IAddSubscriberDto): AppThunkAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: ADD_SUBSCRIBER });

        axios(
        { 
            method: "POST", 
            url: API_COMMAND_ADD_SUBSCRIBER, 
            data: { email: payload.email }
        })
        .then(response => 
        {
            if (response.status === 200)
            {
                dispatch({ type: ADD_SUBSCRIBER_RESPONSE, hasAddedSubscriber: true });
                return;
            }
            
            const error = UnexpectedStatusCode(response.status);
            dispatch({ type: ADD_SUBSCRIBER_ERROR, errorObject: error });
            Sentry.captureException(error);
            
        })
        .catch(error => 
        {
            dispatch({ type: ADD_SUBSCRIBER_ERROR, errorObject: GetErrorMessage(error) });
            Sentry.captureException(error);
        });
    }
}
