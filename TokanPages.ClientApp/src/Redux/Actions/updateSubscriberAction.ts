import * as Sentry from "@sentry/react";
import axios from "axios";
import { AppThunkAction } from "../applicationState";
import { IUpdateSubscriberDto } from "../../Api/Models";
import { API_COMMAND_UPDATE_SUBSCRIBER } from "../../Shared/constants";
import { UnexpectedStatusCode } from "../../Shared/textWrappers";
import { GetErrorMessage } from "../../Shared/helpers";
import { RAISE_ERROR, TErrorActions } from "./raiseErrorAction";

export const UPDATE_SUBSCRIBER = "UPDATE_SUBSCRIBER";
export const UPDATE_SUBSCRIBER_RESPONSE = "UPDATE_SUBSCRIBER_RESPONSE";

export interface IApiUpdateSubscriber { type: typeof UPDATE_SUBSCRIBER }
export interface IApiUpdateSubscriberResponse { type: typeof UPDATE_SUBSCRIBER_RESPONSE }

export type TKnownActions = 
    IApiUpdateSubscriber | 
    IApiUpdateSubscriberResponse | 
    TErrorActions
;

export const ActionCreators = 
{
    updateSubscriber: (payload: IUpdateSubscriberDto): AppThunkAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: UPDATE_SUBSCRIBER });

        axios(
        { 
            method: "POST", 
            url: API_COMMAND_UPDATE_SUBSCRIBER, 
            data: { email: payload.email }
        })
        .then(response => 
        {
            if (response.status === 200)
            {
                dispatch({ type: UPDATE_SUBSCRIBER_RESPONSE });
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
