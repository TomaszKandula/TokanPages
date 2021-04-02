import axios from "axios";
import { AppThunkAction } from "../applicationState";
import { IUpdateSubscriberDto } from "../../Api/Models";
import { API_COMMAND_UPDATE_SUBSCRIBER } from "../../Shared/constants";

export const API_UPDATE_SUBSCRIBER = "API_UPDATE_SUBSCRIBER";
export const API_UPDATE_SUBSCRIBER_RESPONSE = "API_UPDATE_SUBSCRIBER_RESPONSE";

export interface IApiUpdateSubscriber { type: typeof API_UPDATE_SUBSCRIBER }
export interface IApiUpdateSubscriberResponse { type: typeof API_UPDATE_SUBSCRIBER_RESPONSE, hasUpdatedSubscriber: boolean }

export type TKnownActions = IApiUpdateSubscriber | IApiUpdateSubscriberResponse;

export const ActionCreators = 
{
    updateSubscriber: (payLoad: IUpdateSubscriberDto):  AppThunkAction<TKnownActions> => async (dispatch) => 
    {
        dispatch({ type: API_UPDATE_SUBSCRIBER });

        await axios(
        { 
            method: "POST", 
            url: API_COMMAND_UPDATE_SUBSCRIBER, 
            data: 
            { 
                email: payLoad.email 
            }
        })
        .then(function (response) 
        {
            if (response.status === 200)
            {
                dispatch({ type: API_UPDATE_SUBSCRIBER_RESPONSE, hasUpdatedSubscriber: true });
            }
            else
            {
                dispatch({ type: API_UPDATE_SUBSCRIBER_RESPONSE, hasUpdatedSubscriber: false });
                console.log(response.status); //TODO: add proper status code handling
            }
        })
        .catch(function (error) 
        {
            dispatch({ type: API_UPDATE_SUBSCRIBER_RESPONSE, hasUpdatedSubscriber: false });
            console.log(error); //TODO: add proper error handling
        });     
    }
}
