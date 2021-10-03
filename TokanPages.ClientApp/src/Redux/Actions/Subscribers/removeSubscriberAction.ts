import axios from "axios";
import { AppThunkAction } from "../../applicationState";
import { IRemoveSubscriberDto } from "../../../Api/Models";
import { API_COMMAND_REMOVE_SUBSCRIBER, NULL_RESPONSE_ERROR } from "../../../Shared/constants";
import { UnexpectedStatusCode } from "../../../Shared/textWrappers";
import { RaiseError } from "../../../Shared/helpers";
import { TErrorActions } from "./../raiseErrorAction";
import { EnrichConfiguration } from "../../../Api/Request";

export const REMOVE_SUBSCRIBER = "REMOVE_SUBSCRIBER";
export const REMOVE_SUBSCRIBER_RESPONSE = "REMOVE_SUBSCRIBER_RESPONSE";
export interface IApiRemoveSubscriber { type: typeof REMOVE_SUBSCRIBER }
export interface IApiRemoveSubscriberResponse { type: typeof REMOVE_SUBSCRIBER_RESPONSE }
export type TKnownActions = IApiRemoveSubscriber | IApiRemoveSubscriberResponse | TErrorActions;

export const ActionCreators = 
{
    removeSubscriber: (payload: IRemoveSubscriberDto):  AppThunkAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: REMOVE_SUBSCRIBER });

        axios(EnrichConfiguration(
        { 
            method: "POST", 
            url: API_COMMAND_REMOVE_SUBSCRIBER, 
            data: { id: payload.id }
        }))
        .then(response =>
        {
            if (response.status === 200)
            {
                return response.data === null 
                    ? RaiseError(dispatch, NULL_RESPONSE_ERROR) 
                    : dispatch({ type: REMOVE_SUBSCRIBER_RESPONSE });
            }
            
            RaiseError(dispatch, UnexpectedStatusCode(response.status));
        })
        .catch(error =>
        {
            RaiseError(dispatch, error);
        });
    }
}