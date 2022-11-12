import axios from "axios";
import { IApplicationAction } from "../../Configuration";
import { ISendMessageDto } from "../../../Api/Models";
import { NULL_RESPONSE_ERROR } from "../../../Shared/constants";
import { GetTextStatusCode } from "../../../Shared/Services/Utilities";
import { RaiseError } from "../../../Shared/Services/ErrorServices";
import { EnrichConfiguration, SEND_MESSAGE } from "../../../Api/Request";

export const SEND = "SEND_MESSAGE";
export const CLEAR = "SEND_MESSAGE_CLEAR";
export const RESPONSE = "SEND_MESSAGE_RESPONSE";
interface ISend { type: typeof SEND }
interface IClear { type: typeof CLEAR }
interface IResponse { type: typeof RESPONSE; payload: any; }
export type TKnownActions = ISend | IClear | IResponse;

export const ApplicationMessageAction = 
{
    clear: (): IApplicationAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: CLEAR });
    },
    send: (payload: ISendMessageDto): IApplicationAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: SEND });

        axios(EnrichConfiguration(
        { 
            method: "POST", 
            url: SEND_MESSAGE, 
            data: payload
        }))
        .then(response => 
        {
            if (response.status === 200)
            {
                return response.data === null 
                    ? RaiseError({ dispatch: dispatch, errorObject: NULL_RESPONSE_ERROR }) 
                    : dispatch({ type: RESPONSE, payload: response.data })
            }
            
            RaiseError({ dispatch: dispatch, errorObject: GetTextStatusCode({ statusCode: response.status })} );
        })
        .catch(error => 
        {
            RaiseError({ dispatch: dispatch, errorObject: error });
        });     
    }
}