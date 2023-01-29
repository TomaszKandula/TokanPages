import { ApplicationAction } from "../../Configuration";
import { SendMessageDto } from "../../../Api/Models";
import { 
    Execute, 
    GetConfiguration, 
    ExecuteContract, 
    RequestContract, 
    SEND_MESSAGE 
} from "../../../Api/Request";

export const SEND = "SEND_MESSAGE";
export const CLEAR = "SEND_MESSAGE_CLEAR";
export const RESPONSE = "SEND_MESSAGE_RESPONSE";
interface Send { type: typeof SEND }
interface Clear { type: typeof CLEAR }
interface Response { type: typeof RESPONSE; payload: any; }
export type TKnownActions = Send | Clear | Response;

export const ApplicationMessageAction = 
{
    clear: (): ApplicationAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: CLEAR });
    },
    send: (payload: SendMessageDto): ApplicationAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: SEND });

        const request: RequestContract = {
            configuration: {
                method: "POST", 
                url: SEND_MESSAGE, 
                data: payload
            }
        }
    
        const input: ExecuteContract = {
            configuration: GetConfiguration(request),
            dispatch: dispatch,
            responseType: RESPONSE
        }

        Execute(input);  
    }
}