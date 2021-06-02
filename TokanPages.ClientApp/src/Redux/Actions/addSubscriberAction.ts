import * as Sentry from "@sentry/react";
import axios from "axios";
import { AppThunkAction } from "../applicationState";
import { IAddSubscriberDto } from "../../Api/Models";
import { API_COMMAND_ADD_SUBSCRIBER } from "../../Shared/constants";
import { UnexpectedStatusCode } from "../../Shared/textWrappers";
import { GetErrorMessage } from "../../Shared/helpers";
import { RAISE_ERROR, TErrorActions } from "./raiseErrorAction";

export const ADD_SUBSCRIBER = "ADD_SUBSCRIBER";
export const ADD_SUBSCRIBER_CLEAR = "ADD_SUBSCRIBER_CLEAR";
export const ADD_SUBSCRIBER_RESPONSE = "ADD_SUBSCRIBER_RESPONSE";

export interface IApiAddSubscriber { type: typeof ADD_SUBSCRIBER }
export interface IApiAddSubscriberClear { type: typeof ADD_SUBSCRIBER_CLEAR }
export interface IApiAddSubscriberResponse { type: typeof ADD_SUBSCRIBER_RESPONSE }

export type TKnownActions = 
    IApiAddSubscriber | 
    IApiAddSubscriberClear | 
    IApiAddSubscriberResponse | 
    TErrorActions
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
                dispatch({ type: ADD_SUBSCRIBER_RESPONSE });
                return;
            }
            
            const error = UnexpectedStatusCode(response.status);
            dispatch({ type: RAISE_ERROR, errorObject: error });
            Sentry.captureException(error);
            
        })
        .catch(error => 
        {
            dispatch({ type: RAISE_ERROR, errorObject: GetErrorMessage(error) });
            Sentry.captureException(error);
        });
    }
}
