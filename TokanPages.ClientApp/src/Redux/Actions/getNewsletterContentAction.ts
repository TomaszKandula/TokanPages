import axios from "axios";
import * as Sentry from "@sentry/react";
import { AppThunkAction } from "../applicationState";
import { combinedDefaults } from "../../Redux/combinedDefaults";
import { GetErrorMessage } from "../../Shared/helpers";
import { UnexpectedStatusCode } from "../../Shared/textWrappers";
import { GET_NEWSLETTER_CONTENT } from "../../Shared/constants";
import { RAISE_ERROR, TErrorActions } from "./raiseErrorAction";
import { INewsletterContentDto } from "../../Api/Models";

export const REQUEST_NEWSLETTER_CONTENT = "REQUEST_NEWSLETTER_CONTENT";
export const RECEIVE_NEWSLETTER_CONTENT = "RECEIVE_NEWSLETTER_CONTENT";

export interface IRequestNewsletterContent { type: typeof REQUEST_NEWSLETTER_CONTENT }
export interface IReceiveNewsletterContent { type: typeof RECEIVE_NEWSLETTER_CONTENT, payload: INewsletterContentDto }

export type TKnownActions = IRequestNewsletterContent | IReceiveNewsletterContent | TErrorActions;

export const ActionCreators = 
{
    getNewsletterContent: (): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        if (getState().getNewsletterContent.content !== combinedDefaults.getNewsletterContent.content) 
            return;

        dispatch({ type: REQUEST_NEWSLETTER_CONTENT });

        axios( 
        {
            method: "GET", 
            url: GET_NEWSLETTER_CONTENT,
            responseType: "json"
        })
        .then(response =>
        {
            if (response.status === 200)
            {
                dispatch({ type: RECEIVE_NEWSLETTER_CONTENT, payload: response.data });
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