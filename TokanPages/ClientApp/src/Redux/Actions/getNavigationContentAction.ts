import axios from "axios";
import * as Sentry from "@sentry/react";
import { AppThunkAction } from "../applicationState";
import { combinedDefaults } from "../../Redux/combinedDefaults";
import { GetErrorMessage } from "../../Shared/helpers";
import { UnexpectedStatusCode } from "../../Shared/textWrappers";
import { GET_NAVIGATION_CONTENT } from "../../Shared/constants";
import { RAISE_ERROR, TErrorActions } from "./raiseErrorAction";
import { INavigationContentDto } from "../../Api/Models";

export const REQUEST_NAVIGATION_CONTENT = "REQUEST_NAVIGATION_CONTENT";
export const RECEIVE_NAVIGATION_CONTENT = "RECEIVE_NAVIGATION_CONTENT";

export interface IRequestNavigationContent { type: typeof REQUEST_NAVIGATION_CONTENT }
export interface IReceiveNavigationContent { type: typeof RECEIVE_NAVIGATION_CONTENT, payload: INavigationContentDto }

export type TKnownActions = IRequestNavigationContent | IReceiveNavigationContent | TErrorActions;

export const ActionCreators = 
{
    getNavigationContent: (): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        if (getState().getNavigationContent.content !== combinedDefaults.getNavigationContent.content) 
            return;

        dispatch({ type: REQUEST_NAVIGATION_CONTENT });

        axios.get(GET_NAVIGATION_CONTENT, 
        {
            method: "GET", 
            responseType: "json"
        })
        .then(response =>
        {
            if (response.status === 200)
            {
                dispatch({ type: RECEIVE_NAVIGATION_CONTENT, payload: response.data });
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