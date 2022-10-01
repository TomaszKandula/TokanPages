import axios from "axios";
import { IApplicationAction } from "../../Configuration";
import { ISendMessageDto } from "../../../Api/Models";
import { API_COMMAND_SEND_MESSAGE, NULL_RESPONSE_ERROR } from "../../../Shared/constants";
import { GetTextStatusCode } from "../../../Shared/Services/Utilities";
import { RaiseError } from "../../../Shared/Services/ErrorServices";
import { TErrorActions } from "./applicationError";
import { EnrichConfiguration } from "../../../Api/Request";

export const API_SEND_MESSAGE = "API_SEND_MESSAGE";
export const API_SEND_MESSAGE_CLEAR = "API_SEND_MESSAGE_CLEAR";
export const API_SEND_MESSAGE_RESPONSE = "API_SEND_MESSAGE_RESPONSE";
export interface IApiSendMessage { type: typeof API_SEND_MESSAGE }
export interface IApiSendMessageClear { type: typeof API_SEND_MESSAGE_CLEAR }
export interface IApiSendMessageResponse { type: typeof API_SEND_MESSAGE_RESPONSE }
export type TKnownActions = IApiSendMessage | IApiSendMessageClear | IApiSendMessageResponse |TErrorActions;

export const ApplicationMessageAction = 
{
    clear: (): IApplicationAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: API_SEND_MESSAGE_CLEAR });
    },
    send: (payload: ISendMessageDto): IApplicationAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: API_SEND_MESSAGE });

        axios(EnrichConfiguration(
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
        }))
        .then(response => 
        {
            if (response.status === 200)
            {
                return response.data === null 
                    ? RaiseError({ dispatch: dispatch, errorObject: NULL_RESPONSE_ERROR }) 
                    : dispatch({ type: API_SEND_MESSAGE_RESPONSE })
            }
            
            RaiseError({ dispatch: dispatch, errorObject: GetTextStatusCode({ statusCode: response.status })} );
        })
        .catch(error => 
        {
            RaiseError({ dispatch: dispatch, errorObject: error });
        });     
    }
}