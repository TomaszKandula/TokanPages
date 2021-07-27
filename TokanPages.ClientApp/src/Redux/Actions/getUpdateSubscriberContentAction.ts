import axios from "axios";
import { AppThunkAction } from "../applicationState";
import { combinedDefaults } from "../../Redux/combinedDefaults";
import { RaiseError } from "../../Shared/helpers";
import { UnexpectedStatusCode } from "../../Shared/textWrappers";
import { GET_UPDATE_SUBSCRIBER_CONTENT, NULL_RESPONSE_ERROR } from "../../Shared/constants";
import { TErrorActions } from "./raiseErrorAction";
import { IUpdateSubscriberContentDto } from "../../Api/Models";

export const REQUEST_UPDATE_SUBSCRIBER_CONTENT = "REQUEST_UPDATE_SUBSCRIBER_CONTENT";
export const RECEIVE_UPDATE_SUBSCRIBER_CONTENT = "RECEIVE_UPDATE_SUBSCRIBER_CONTENT";
export interface IRequestUpdateSubscriberContent { type: typeof REQUEST_UPDATE_SUBSCRIBER_CONTENT }
export interface IReceiveUpdateSubscriberContent { type: typeof RECEIVE_UPDATE_SUBSCRIBER_CONTENT, payload: IUpdateSubscriberContentDto }
export type TKnownActions = IRequestUpdateSubscriberContent | IReceiveUpdateSubscriberContent | TErrorActions;

export const ActionCreators = 
{
    getUpdateSubscriberContent: (): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        if (getState().getUpdateSubscriberContent.content !== combinedDefaults.getUpdateSubscriberContent.content) 
            return;
        
        dispatch({ type: REQUEST_UPDATE_SUBSCRIBER_CONTENT });

        axios( 
        {
            method: "GET", 
            url: GET_UPDATE_SUBSCRIBER_CONTENT,
            responseType: "json"
        })
        .then(response =>
        {
            if (response.status === 200)
            {
                return response.data === null 
                    ? RaiseError(dispatch, NULL_RESPONSE_ERROR) 
                    : dispatch({ type: RECEIVE_UPDATE_SUBSCRIBER_CONTENT, payload: response.data });
            }

            RaiseError(dispatch, UnexpectedStatusCode(response.status));
        })
        .catch(error =>
        {
            RaiseError(dispatch, error);
        });
    }
}