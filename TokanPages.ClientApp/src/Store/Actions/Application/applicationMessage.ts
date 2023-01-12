import { IApplicationAction } from "../../Configuration";
import { ISendMessageDto } from "../../../Api/Models";
import { 
    Execute, 
    GetConfiguration, 
    IExecute, 
    IRequest, 
    SEND_MESSAGE 
} from "../../../Api/Request";

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

        const request: IRequest = {
            configuration: {
                method: "POST", 
                url: SEND_MESSAGE, 
                data: payload
            }
        }
    
        const input: IExecute = {
            configuration: GetConfiguration(request),
            dispatch: dispatch,
            responseType: RESPONSE
        }

        Execute(input);  
    }
}