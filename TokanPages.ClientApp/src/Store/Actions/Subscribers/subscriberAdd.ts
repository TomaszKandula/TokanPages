import { IApplicationAction } from "../../Configuration";
import { IAddSubscriberDto } from "../../../Api/Models";
import { 
    Execute, 
    GetConfiguration, 
    IExecute, 
    IRequest, 
    ADD_SUBSCRIBER
} from "../../../Api/Request";

export const ADD = "ADD_SUBSCRIBER";
export const CLEAR = "ADD_SUBSCRIBER_CLEAR";
export const RESPONSE = "ADD_SUBSCRIBER_RESPONSE";
interface IAdd { type: typeof ADD }
interface IClear { type: typeof CLEAR }
interface IResponse { type: typeof RESPONSE; payload: any; }
export type TKnownActions = IAdd | IClear | IResponse;

export const SubscriberAddAction = 
{
    clear: (): IApplicationAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: CLEAR });
    },    
    add: (payload: IAddSubscriberDto): IApplicationAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: ADD });

        const request: IRequest = {
            configuration: {
                method: "POST", 
                url: ADD_SUBSCRIBER, 
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