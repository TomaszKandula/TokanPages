import axios from "axios";
import { AppThunkAction } from "../../applicationState";
import { combinedDefaults } from "../../../Redux/combinedDefaults";
import { RaiseError } from "../../../Shared/helpers";
import { UnexpectedStatusCode } from "../../../Shared/textWrappers";
import { GET_FEATURED_CONTENT, NULL_RESPONSE_ERROR } from "../../../Shared/constants";
import { TErrorActions } from "./../raiseErrorAction";
import { IFeaturedContentDto } from "../../../Api/Models";
import { EnrichConfiguration } from "../../../Api/Request";

export const REQUEST_FEATURED_CONTENT = "REQUEST_FEATURED_CONTENT";
export const RECEIVE_FEATURED_CONTENT = "RECEIVE_FEATURED_CONTENT";
export interface IRequestFeaturedContent { type: typeof REQUEST_FEATURED_CONTENT }
export interface IReceiveFeaturedContent { type: typeof RECEIVE_FEATURED_CONTENT, payload: IFeaturedContentDto }
export type TKnownActions = IRequestFeaturedContent | IReceiveFeaturedContent | TErrorActions;

export const ActionCreators = 
{
    getFeaturedContent: (): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        if (getState().getFeaturedContent.content !== combinedDefaults.getFeaturedContent.content) 
            return;

        dispatch({ type: REQUEST_FEATURED_CONTENT });

        axios(EnrichConfiguration(
        {
            method: "GET", 
            url: GET_FEATURED_CONTENT,
            responseType: "json"
        }))
        .then(response =>
        {
            if (response.status === 200)
            {
                return response.data === null 
                    ? RaiseError(dispatch, NULL_RESPONSE_ERROR) 
                    : dispatch({ type: RECEIVE_FEATURED_CONTENT, payload: response.data });
            }
            
            RaiseError(dispatch, UnexpectedStatusCode(response.status));
        })
        .catch(error =>
        {
            RaiseError(dispatch, error);
        });
    }
}