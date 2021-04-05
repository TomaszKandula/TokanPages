import axios from "axios";
import { AppThunkAction } from "../applicationState";
import { IAddSubscriberDto } from "../../Api/Models";
import { API_COMMAND_ADD_SUBSCRIBER } from "../../Shared/constants";

export const ADD_SUBSCRIBER = "ADD_SUBSCRIBER";
export const ADD_SUBSCRIBER_CLEAR = "ADD_SUBSCRIBER_CLEAR";
export const ADD_SUBSCRIBER_RESPONSE = "ADD_SUBSCRIBER_RESPONSE";
export const ADD_SUBSCRIBER_ERROR = "ADD_SUBSCRIBER_ERROR";

export interface IApiAddSubscriber { type: typeof ADD_SUBSCRIBER }
export interface IApiAddSubscriberClear { type: typeof ADD_SUBSCRIBER_CLEAR }
export interface IApiAddSubscriberResponse { type: typeof ADD_SUBSCRIBER_RESPONSE, hasAddedSubscriber: boolean }
export interface IAddSubscriberError { type: typeof ADD_SUBSCRIBER_ERROR, errorObject: any }

export type TKnownActions = 
    IApiAddSubscriber | 
    IApiAddSubscriberClear | 
    IApiAddSubscriberResponse | 
    IAddSubscriberError
;

export const ActionCreators = 
{
    addSubscriberClear: ():  AppThunkAction<TKnownActions> => async (dispatch) => 
    {
        dispatch({ type: ADD_SUBSCRIBER_CLEAR });
    },    
    addSubscriber: (payload: IAddSubscriberDto):  AppThunkAction<TKnownActions> => async (dispatch) => 
    {
        dispatch({ type: ADD_SUBSCRIBER });

        await axios(
        { 
            method: "POST", 
            url: API_COMMAND_ADD_SUBSCRIBER, 
            data: { email: payload.email }
        })
        .then(response => 
        {
            return response.status === 200 
                ? dispatch({ type: ADD_SUBSCRIBER_RESPONSE, hasAddedSubscriber: true })
                : dispatch({ type: ADD_SUBSCRIBER_ERROR, errorObject: "Unexpected status code" });//TODO: add object error for unexpected status code
        })
        .catch(error => 
        {
            dispatch({ type: ADD_SUBSCRIBER_ERROR, errorObject: error });
        });
    }
}
