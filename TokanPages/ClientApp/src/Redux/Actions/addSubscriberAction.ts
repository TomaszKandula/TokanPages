import axios from "axios";
import { AppThunkAction } from "../applicationState";
import { IAddSubscriberDto } from "../../Api/Models";
import { API_COMMAND_ADD_SUBSCRIBER } from "../../Shared/constants";

export const API_ADD_SUBSCRIBER = "API_ADD_SUBSCRIBER";
export const API_ADD_SUBSCRIBER_RESPONSE = "API_ADD_SUBSCRIBER_RESPONSE";

export interface IApiAddSubscriber { type: typeof API_ADD_SUBSCRIBER }
export interface IApiAddSubscriberResponse { type: typeof API_ADD_SUBSCRIBER_RESPONSE, hasAddedSubscriber: boolean }

export type TKnownActions = IApiAddSubscriber | IApiAddSubscriberResponse;

export const ActionCreators = 
{
    addSubscriber: (payLoad: IAddSubscriberDto):  AppThunkAction<TKnownActions> => async (dispatch) => 
    {
        dispatch({ type: API_ADD_SUBSCRIBER });

        await axios(
        { 
            method: "POST", 
            url: API_COMMAND_ADD_SUBSCRIBER, 
            data: 
            { 
                email: payLoad.email 
            }
        })
        .then(function (response) 
        {
            if (response.status === 200)
            {
                dispatch({ type: API_ADD_SUBSCRIBER_RESPONSE, hasAddedSubscriber: true });
            }
            else
            {
                dispatch({ type: API_ADD_SUBSCRIBER_RESPONSE, hasAddedSubscriber: false });
                console.log(response.status); //TODO: add proper status code handling
            }
        })
        .catch(function (error) 
        {
            dispatch({ type: API_ADD_SUBSCRIBER_RESPONSE, hasAddedSubscriber: false });
            console.log(error); //TODO: add proper error handling
        });     
    }
}
