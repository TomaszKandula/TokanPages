import axios from "axios";
import { IApplicationAction } from "../../Configuration";
import { IAddSubscriberDto } from "../../../Api/Models";
import { NULL_RESPONSE_ERROR } from "../../../Shared/constants";
import { GetTextStatusCode } from "../../../Shared/Services/Utilities";
import { RaiseError } from "../../../Shared/Services/ErrorServices";
import { EnrichConfiguration, ADD_SUBSCRIBER } from "../../../Api/Request";

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

        axios(EnrichConfiguration(
        { 
            method: "POST", 
            url: ADD_SUBSCRIBER, 
            data: payload
        }))
        .then(response => 
        {
            if (response.status === 200)
            {
                return response.data === null 
                    ? RaiseError({ dispatch: dispatch, errorObject: NULL_RESPONSE_ERROR }) 
                    : dispatch({ type: RESPONSE, payload: response.data });
            }
            
            RaiseError({ dispatch: dispatch, errorObject: GetTextStatusCode({ statusCode: response.status }) });
        })
        .catch(error => 
        {
            RaiseError({ dispatch: dispatch, errorObject: error });
        });
    }
}