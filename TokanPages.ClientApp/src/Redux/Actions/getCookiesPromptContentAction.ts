import axios from "axios";
import * as Sentry from "@sentry/react";
import { AppThunkAction } from "../applicationState";
import { combinedDefaults } from "../../Redux/combinedDefaults";
import { GetErrorMessage } from "../../Shared/helpers";
import { UnexpectedStatusCode } from "../../Shared/textWrappers";
import { GET_COOKIES_PROMPT_CONTENT } from "../../Shared/constants";
import { RAISE_ERROR, TErrorActions } from "./raiseErrorAction";
import { ICookiesPromptContentDto } from "../../Api/Models";

export const REQUEST_COOKIES_PROMPT_CONTENT = "REQUEST_COOKIES_PROMPT_CONTENT";
export const RECEIVE_COOKIES_PROMPT_CONTENT = "RECEIVE_COOKIES_PROMPT_CONTENT";

export interface IRequestCookiesPromptContent { type: typeof REQUEST_COOKIES_PROMPT_CONTENT }
export interface IReceiveCookiesPromptContent { type: typeof RECEIVE_COOKIES_PROMPT_CONTENT, payload: ICookiesPromptContentDto }

export type TKnownActions = IRequestCookiesPromptContent | IReceiveCookiesPromptContent | TErrorActions;

export const ActionCreators = 
{
    getCookiesPromptContent: (): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        if (getState().getCookiesPromptContent.content !== combinedDefaults.getCookiesPromptContent.content) 
            return;

        dispatch({ type: REQUEST_COOKIES_PROMPT_CONTENT });

        axios( 
        {
            method: "GET", 
            url: GET_COOKIES_PROMPT_CONTENT,
            responseType: "json"
        })
        .then(response =>
        {
            if (response.status === 200)
            {
                dispatch({ type: RECEIVE_COOKIES_PROMPT_CONTENT, payload: response.data });
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