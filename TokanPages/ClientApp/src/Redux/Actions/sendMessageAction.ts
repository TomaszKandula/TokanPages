import axios from "axios";
import { AppThunkAction } from "../../Redux/applicationState";
import { ISendMessageDto } from "../../Api/Models";
import { API_COMMAND_SEND_MESSAGE } from "../../Shared/constants";

export const API_SEND_MESSAGE = "API_SEND_MESSAGE";
export const API_SEND_MESSAGE_RESPONSE = "API_SEND_MESSAGE_RESPONSE";

export interface IApiSendMessage { type: typeof API_SEND_MESSAGE }
export interface IApiSendMessageResponse { type: typeof API_SEND_MESSAGE_RESPONSE, hasSentMessage: boolean }

export type TKnownActions = IApiSendMessage | IApiSendMessageResponse;

export const ActionCreators = 
{
    sendMessage: (PayLoad: ISendMessageDto):  AppThunkAction<TKnownActions> => async (dispatch) => 
    {
        dispatch({ type: API_SEND_MESSAGE });

        await axios(
        { 
            method: "POST", 
            url: API_COMMAND_SEND_MESSAGE, 
            data: 
            { 
                firstName: PayLoad.firstName,
                lastName:  PayLoad.lastName,
                userEmail: PayLoad.userEmail,
                emailFrom: PayLoad.emailFrom,
                emailTos:  PayLoad.emailTos,
                subject:   PayLoad.subject,
                message:   PayLoad.message
            }
        })
        .then(function (response) 
        {
            if (response.status === 200)
            {
                dispatch({ type: API_SEND_MESSAGE_RESPONSE, hasSentMessage: true });
            }
            else
            {
                dispatch({ type: API_SEND_MESSAGE_RESPONSE, hasSentMessage: false });
                console.log(response.status); //TODO: add proper status code handling
            }
        })
        .catch(function (error) 
        {
            dispatch({ type: API_SEND_MESSAGE_RESPONSE, hasSentMessage: false });
            console.log(error); //TODO: add proper error handling
        });     
    }
}
