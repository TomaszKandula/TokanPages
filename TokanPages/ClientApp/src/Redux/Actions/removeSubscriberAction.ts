import axios from "axios";
import { AppThunkAction } from "../applicationState";
import { IRemoveSubscriberDto } from "../../Api/Models";
import { API_COMMAND_REMOVE_SUBSCRIBER } from "../../Shared/constants";

export const API_REMOVE_SUBSCRIBER = "API_REMOVE_SUBSCRIBER";
export const API_REMOVE_SUBSCRIBER_RESPONSE = "API_REMOVE_SUBSCRIBER_RESPONSE";

export interface IApiRemoveSubscriber { type: typeof API_REMOVE_SUBSCRIBER }
export interface IApiRemoveSubscriberResponse { type: typeof API_REMOVE_SUBSCRIBER_RESPONSE, hasRemovedSubscriber: boolean }

export type TKnownActions = IApiRemoveSubscriber | IApiRemoveSubscriberResponse;

export const ActionCreators = 
{
    removeSubscriber: (payload: IRemoveSubscriberDto):  AppThunkAction<TKnownActions> => async (dispatch) => 
    {
        dispatch({ type: API_REMOVE_SUBSCRIBER });

        await axios(
        { 
            method: "POST", 
            url: API_COMMAND_REMOVE_SUBSCRIBER, 
            data: 
            { 
                id: payload.id
            }
        })
        .then(function (response) 
        {
            if (response.status === 200)
            {
                dispatch({ type: API_REMOVE_SUBSCRIBER_RESPONSE, hasRemovedSubscriber: true });
            }
            else
            {
                dispatch({ type: API_REMOVE_SUBSCRIBER_RESPONSE, hasRemovedSubscriber: false });
                console.log(response.status); //TODO: add proper status code handling
            }
        })
        .catch(function (error) 
        {
            dispatch({ type: API_REMOVE_SUBSCRIBER_RESPONSE, hasRemovedSubscriber: false });
            console.log(error); //TODO: add proper error handling
        });     
    }
}
