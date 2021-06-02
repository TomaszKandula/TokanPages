import * as Sentry from "@sentry/react";
import axios from "axios";
import { AppThunkAction } from "../applicationState";
import { IRemoveSubscriberDto } from "../../Api/Models";
import { API_COMMAND_REMOVE_SUBSCRIBER } from "../../Shared/constants";
import { UnexpectedStatusCode } from "../../Shared/textWrappers";
import { GetErrorMessage } from "../../Shared/helpers";
import { RAISE_ERROR, TErrorActions } from "./raiseErrorAction";

export const REMOVE_SUBSCRIBER = "REMOVE_SUBSCRIBER";
export const REMOVE_SUBSCRIBER_RESPONSE = "REMOVE_SUBSCRIBER_RESPONSE";

export interface IApiRemoveSubscriber { type: typeof REMOVE_SUBSCRIBER }
export interface IApiRemoveSubscriberResponse { type: typeof REMOVE_SUBSCRIBER_RESPONSE }

export type TKnownActions = 
    IApiRemoveSubscriber | 
    IApiRemoveSubscriberResponse | 
    TErrorActions
;

export const ActionCreators = 
{
    removeSubscriber: (payload: IRemoveSubscriberDto):  AppThunkAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: REMOVE_SUBSCRIBER });

        axios(
        { 
            method: "POST", 
            url: API_COMMAND_REMOVE_SUBSCRIBER, 
            data: { id: payload.id }
        })
        .then(response =>
        {
            if (response.status === 200)
            {
                dispatch({ type: REMOVE_SUBSCRIBER_RESPONSE });
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
