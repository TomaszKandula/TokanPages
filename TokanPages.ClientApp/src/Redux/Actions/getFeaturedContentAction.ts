import axios from "axios";
import * as Sentry from "@sentry/react";
import { AppThunkAction } from "../applicationState";
import { combinedDefaults } from "../../Redux/combinedDefaults";
import { GetErrorMessage } from "../../Shared/helpers";
import { UnexpectedStatusCode } from "../../Shared/textWrappers";
import { GET_FEATURED_CONTENT } from "../../Shared/constants";
import { RAISE_ERROR, TErrorActions } from "./raiseErrorAction";
import { IFeaturedContentDto } from "../../Api/Models";

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

        axios.get(GET_FEATURED_CONTENT, 
        {
            method: "GET", 
            responseType: "json"
        })
        .then(response =>
        {
            if (response.status === 200)
            {
                dispatch({ type: RECEIVE_FEATURED_CONTENT, payload: response.data });
                return;
            }
            
            const error = UnexpectedStatusCode(response.status);
            dispatch({ type: RAISE_ERROR, errorObject: error });
            Sentry.captureException(error);
        })
        .catch(error =>
        {
            dispatch({ type: RAISE_ERROR, errorObject: GetErrorMessage(error) });
            Sentry.captureException(error);
        });
    }
}