import { ApplicationAction } from "../../Configuration";
import { RemoveSubscriberDto } from "../../../Api/Models";
import { 
    Execute, 
    GetConfiguration, 
    ExecuteContract, 
    RequestContract, 
    REMOVE_SUBSCRIBER
} from "../../../Api/Request";

export const REMOVE = "REMOVE_SUBSCRIBER";
export const RESPONSE = "REMOVE_SUBSCRIBER_RESPONSE";
interface Remove { type: typeof REMOVE }
interface Response { type: typeof RESPONSE; payload: any; }
export type TKnownActions = Remove | Response;

export const SubscriberRemoveAction = 
{
    remove: (payload: RemoveSubscriberDto):  ApplicationAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: REMOVE });

        const request: RequestContract = {
            configuration: {
                method: "POST", 
                url: REMOVE_SUBSCRIBER, 
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