import axios from "axios";
import * as Sentry from "@sentry/react";
import { AppThunkAction } from "../applicationState";
import { combinedDefaults } from "../../Redux/combinedDefaults";
import { GetErrorMessage } from "../../Shared/helpers";
import { UnexpectedStatusCode } from "../../Shared/textWrappers";
import { GET_UNSUBSCRIBE_CONTENT } from "../../Shared/constants";
import { RAISE_ERROR, TErrorActions } from "./raiseErrorAction";
import { IUnsubscribeContentDto } from "../../Api/Models";

export const REQUEST_UNSUBSCRIBE_CONTENT = "REQUEST_UNSUBSCRIBE_CONTENT";
export const RECEIVE_UNSUBSCRIBE_CONTENT = "RECEIVE_UNSUBSCRIBE_CONTENT";

export interface IRequestUnsubscribeContent { type: typeof REQUEST_UNSUBSCRIBE_CONTENT }
export interface IReceiveUnsubscribeContent { type: typeof RECEIVE_UNSUBSCRIBE_CONTENT, payload: IUnsubscribeContentDto }

export type TKnownActions = IRequestUnsubscribeContent | IReceiveUnsubscribeContent | TErrorActions;

export const ActionCreators = 
{
    getUnsubscribeContent: (): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        if (getState().getUnsubscribeContent.content !== combinedDefaults.getUnsubscribeContent.content) 
            return;
        
        dispatch({ type: REQUEST_UNSUBSCRIBE_CONTENT });

        axios.get(GET_UNSUBSCRIBE_CONTENT, 
        {
            method: "GET", 
            responseType: "json"
        })
        .then(response =>
        {
            if (response.status === 200)
            {
                dispatch({ type: RECEIVE_UNSUBSCRIBE_CONTENT, payload: response.data });
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