import axios from "axios";
import { AppThunkAction } from "../../Redux/applicationState";
import { ISendMessageDto } from "../../Api/Models";
import { API_COMMAND_SEND_MESSAGE, NULL_RESPONSE_ERROR } from "../../Shared/constants";
import { UnexpectedStatusCode } from "../../Shared/textWrappers";
import { RaiseError } from "../../Shared/helpers";
import { TErrorActions } from "./raiseErrorAction";

export const API_SEND_MESSAGE = "API_SEND_MESSAGE";
export const API_SEND_MESSAGE_CLEAR = "API_SEND_MESSAGE_CLEAR";
export const API_SEND_MESSAGE_RESPONSE = "API_SEND_MESSAGE_RESPONSE";
export interface IApiSendMessage { type: typeof API_SEND_MESSAGE }
export interface IApiSendMessageClear { type: typeof API_SEND_MESSAGE_CLEAR }
export interface IApiSendMessageResponse { type: typeof API_SEND_MESSAGE_RESPONSE }
export type TKnownActions = IApiSendMessage | IApiSendMessageClear | IApiSendMessageResponse |TErrorActions;

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
                lastName: payload.lastName,
                userEmail: payload.userEmail,
                emailFrom: payload.emailFrom,
                emailTos: payload.emailTos,
                subject: payload.subject,
                message: payload.message
            }
        })
        .then(response => 
        {
            if (response.status === 200)
            {
                return response.data === null 
                    ? RaiseError(dispatch, NULL_RESPONSE_ERROR) 
                    : dispatch({ type: API_SEND_MESSAGE_RESPONSE })
            }
            
            RaiseError(dispatch, UnexpectedStatusCode(response.status));
        })
        .catch(error => 
        {
            RaiseError(dispatch, error);
        });     
    }
}