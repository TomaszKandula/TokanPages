import { IApplicationAction } from "../../Configuration";
import { IUpdateSubscriberDto } from "../../../Api/Models";
import { 
    Execute, 
    GetConfiguration, 
    IExecute, 
    IRequest, 
    UPDATE_SUBSCRIBER
} from "../../../Api/Request";

export const UPDATE = "UPDATE_SUBSCRIBER";
export const RESPONSE = "UPDATE_SUBSCRIBER_RESPONSE";
interface IUpdate { type: typeof UPDATE }
interface IResponse { type: typeof RESPONSE; payload: any; }
export type TKnownActions = IUpdate | IResponse;

export const SubscriberUpdateAction = 
{
    update: (payload: IUpdateSubscriberDto): IApplicationAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: UPDATE });

        const request: IRequest = {
            configuration: {
                method: "POST", 
                url: UPDATE_SUBSCRIBER, 
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