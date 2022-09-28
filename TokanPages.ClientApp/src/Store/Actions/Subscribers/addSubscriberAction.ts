import axios from "axios";
import { AppThunkAction } from "../../applicationState";
import { IAddSubscriberDto } from "../../../Api/Models";
import { API_COMMAND_ADD_SUBSCRIBER, NULL_RESPONSE_ERROR } from "../../../Shared/constants";
import { GetTextStatusCode } from "../../../Shared/Services/Utilities";
import { RaiseError } from "../../../Shared/Services/ErrorServices";
import { TErrorActions } from "../raiseErrorAction";
import { EnrichConfiguration } from "../../../Api/Request";

export const ADD_SUBSCRIBER = "ADD_SUBSCRIBER";
export const ADD_SUBSCRIBER_CLEAR = "ADD_SUBSCRIBER_CLEAR";
export const ADD_SUBSCRIBER_RESPONSE = "ADD_SUBSCRIBER_RESPONSE";
export interface IApiAddSubscriber { type: typeof ADD_SUBSCRIBER }
export interface IApiAddSubscriberClear { type: typeof ADD_SUBSCRIBER_CLEAR }
export interface IApiAddSubscriberResponse { type: typeof ADD_SUBSCRIBER_RESPONSE }
export type TKnownActions = IApiAddSubscriber | IApiAddSubscriberClear | IApiAddSubscriberResponse | TErrorActions;

export const ActionCreators = 
{
    addSubscriberClear: (): AppThunkAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: ADD_SUBSCRIBER_CLEAR });
    },    
    addSubscriber: (payload: IAddSubscriberDto): AppThunkAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: ADD_SUBSCRIBER });

        axios(EnrichConfiguration(
        { 
            method: "POST", 
            url: API_COMMAND_ADD_SUBSCRIBER, 
            data: { email: payload.email }
        }))
        .then(response => 
        {
            if (response.status === 200)
            {
                return response.data === null 
                    ? RaiseError({ dispatch: dispatch, errorObject: NULL_RESPONSE_ERROR }) 
                    : dispatch({ type: ADD_SUBSCRIBER_RESPONSE });
            }
            
            RaiseError({ dispatch: dispatch, errorObject: GetTextStatusCode({ statusCode: response.status }) });
        })
        .catch(error => 
        {
            RaiseError({ dispatch: dispatch, errorObject: error });
        });
    }
}