import * as Sentry from "@sentry/react";
import axios from "axios";
import { AppThunkAction } from "../../Redux/applicationState";
import { ISendMessageDto } from "../../Api/Models";
import { API_COMMAND_SEND_MESSAGE } from "../../Shared/constants";
import { UnexpectedStatusCode } from "../../Shared/textWrappers";
import { GetErrorMessage } from "../../Shared/helpers";
import { RAISE_ERROR, TErrorActions } from "./raiseErrorAction";

export const API_SEND_MESSAGE = "API_SEND_MESSAGE";
export const API_SEND_MESSAGE_CLEAR = "API_SEND_MESSAGE_CLEAR";
export const API_SEND_MESSAGE_RESPONSE = "API_SEND_MESSAGE_RESPONSE";

export interface IApiSendMessage { type: typeof API_SEND_MESSAGE }
export interface IApiSendMessageClear { type: typeof API_SEND_MESSAGE_CLEAR }
export interface IApiSendMessageResponse { type: typeof API_SEND_MESSAGE_RESPONSE }

export type TKnownActions = 
    IApiSendMessage | 
    IApiSendMessageClear | 
    IApiSendMessageResponse |
    TErrorActions
;

export const ActionCreators = 
{
    sendMessageClear: (): AppThunkAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: API_SEND_MESSAGE_CLEAR });
    },
    sendMessage: (payload: ISendMessageDto): AppThunkAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: API_SEND_MESSAGE });

        axios(
        { 
            method: "POST", 
            url: API_COMMAND_SEND_MESSAGE, 
            data: 
            { 
                firstName: payload.firstName,
                lastName:  payload.lastName,
                userEmail: payload.userEmail,
                emailFrom: payload.emailFrom,
                emailTos:  payload.emailTos,
                subject:   payload.subject,
                message:   payload.message
            }
        })
        .then(response => 
        {
            if (response.status === 200)
            {
                dispatch({ type: API_SEND_MESSAGE_RESPONSE })
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
