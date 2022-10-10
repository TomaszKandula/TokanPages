import axios from "axios";
import { IApplicationAction } from "../../Configuration";
import { ISendMessageDto } from "../../../Api/Models";
import { API_COMMAND_SEND_MESSAGE, NULL_RESPONSE_ERROR } from "../../../Shared/constants";
import { GetTextStatusCode } from "../../../Shared/Services/Utilities";
import { RaiseError } from "../../../Shared/Services/ErrorServices";
import { EnrichConfiguration } from "../../../Api/Request";

export const SEND_MESSAGE = "SEND_MESSAGE";
export const SEND_MESSAGE_CLEAR = "SEND_MESSAGE_CLEAR";
export const SEND_MESSAGE_RESPONSE = "SEND_MESSAGE_RESPONSE";
export interface ISendMessage { type: typeof SEND_MESSAGE }
export interface ISendMessageClear { type: typeof SEND_MESSAGE_CLEAR }
export interface ISendMessageResponse { type: typeof SEND_MESSAGE_RESPONSE }
export type TKnownActions = ISendMessage | ISendMessageClear | ISendMessageResponse;

export const ApplicationMessageAction = 
{
    clear: (): IApplicationAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: SEND_MESSAGE_CLEAR });
    },
    send: (payload: ISendMessageDto): IApplicationAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: SEND_MESSAGE });

        axios(EnrichConfiguration(
        { 
            method: "POST", 
            url: API_COMMAND_SEND_MESSAGE, 
            data: payload
        }))
        .then(response => 
        {
            if (response.status === 200)
            {
                return response.data === null 
                    ? RaiseError({ dispatch: dispatch, errorObject: NULL_RESPONSE_ERROR }) 
                    : dispatch({ type: SEND_MESSAGE_RESPONSE })
            }
            
            RaiseError({ dispatch: dispatch, errorObject: GetTextStatusCode({ statusCode: response.status })} );
        })
        .catch(error => 
        {
            RaiseError({ dispatch: dispatch, errorObject: error });
        });     
    }
}