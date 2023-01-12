import { IApplicationAction } from "../../Configuration";
import { IRemoveSubscriberDto } from "../../../Api/Models";
import { 
    Execute, 
    GetConfiguration, 
    IExecute, 
    IRequest, 
    REMOVE_SUBSCRIBER
} from "../../../Api/Request";

export const REMOVE = "REMOVE_SUBSCRIBER";
export const RESPONSE = "REMOVE_SUBSCRIBER_RESPONSE";
interface IRemove { type: typeof REMOVE }
interface IResponse { type: typeof RESPONSE; payload: any; }
export type TKnownActions = IRemove | IResponse;

export const SubscriberRemoveAction = 
{
    remove: (payload: IRemoveSubscriberDto):  IApplicationAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: REMOVE });

        const request: IRequest = {
            configuration: {
                method: "POST", 
                url: REMOVE_SUBSCRIBER, 
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