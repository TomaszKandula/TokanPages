import { ApplicationAction } from "../../Configuration";
import { AddSubscriberDto } from "../../../Api/Models";
import { 
    Execute, 
    GetConfiguration, 
    ExecuteContract, 
    RequestContract, 
    ADD_SUBSCRIBER
} from "../../../Api/Request";

export const ADD = "ADD_SUBSCRIBER";
export const CLEAR = "ADD_SUBSCRIBER_CLEAR";
export const RESPONSE = "ADD_SUBSCRIBER_RESPONSE";
interface Add { type: typeof ADD }
interface Clear { type: typeof CLEAR }
interface Response { type: typeof RESPONSE; payload: any; }
export type TKnownActions = Add | Clear | Response;

export const SubscriberAddAction = 
{
    clear: (): ApplicationAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: CLEAR });
    },    
    add: (payload: AddSubscriberDto): ApplicationAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: ADD });

        const request: RequestContract = {
            configuration: {
                method: "POST", 
                url: ADD_SUBSCRIBER, 
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