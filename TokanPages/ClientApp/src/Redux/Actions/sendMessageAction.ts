import axios from "axios";
import { AppThunkAction } from "../../Redux/applicationState";
import { ISendMessageDto } from "../../Api/Models";
import { API_COMMAND_SEND_MESSAGE } from "../../Shared/constants";
import { GetUnexpectedStatusCode } from "../../Shared/messageHelper";

export const API_SEND_MESSAGE = "API_SEND_MESSAGE";
export const API_SEND_MESSAGE_CLEAR = "API_SEND_MESSAGE_CLEAR";
export const API_SEND_MESSAGE_RESPONSE = "API_SEND_MESSAGE_RESPONSE";
export const SEND_MESSAGE_ERROR = "SEND_MESSAGE_ERROR";

export interface IApiSendMessage { type: typeof API_SEND_MESSAGE }
export interface IApiSendMessageClear { type: typeof API_SEND_MESSAGE_CLEAR }
export interface IApiSendMessageResponse { type: typeof API_SEND_MESSAGE_RESPONSE, hasSentMessage: boolean }
export interface ISendMessageError { type: typeof SEND_MESSAGE_ERROR, errorObject: any }

export type TKnownActions = 
    IApiSendMessage | 
    IApiSendMessageClear | 
    IApiSendMessageResponse |
    ISendMessageError
;

export const ActionCreators = 
{
    sendMessageClear: ():  AppThunkAction<TKnownActions> => async (dispatch) => 
    {
        dispatch({ type: API_SEND_MESSAGE_CLEAR });
    },
    sendMessage: (payload: ISendMessageDto):  AppThunkAction<TKnownActions> => async (dispatch) => 
    {
        dispatch({ type: API_SEND_MESSAGE });

        await axios(
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
            return response.status === 200
                ? dispatch({ type: API_SEND_MESSAGE_RESPONSE, hasSentMessage: true })
                : dispatch({ type: SEND_MESSAGE_ERROR, errorObject: GetUnexpectedStatusCode(response.status) });
 
        })
        .catch(error => 
        {
            dispatch({ type: SEND_MESSAGE_ERROR, errorObject: error });
        });     
    }
}
