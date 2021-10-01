import axios from "axios";
import { AppThunkAction } from "../../applicationState";
import { combinedDefaults } from "../../../Redux/combinedDefaults";
import { RaiseError } from "../../../Shared/helpers";
import { UnexpectedStatusCode } from "../../../Shared/textWrappers";
import { GET_COOKIES_PROMPT_CONTENT, NULL_RESPONSE_ERROR } from "../../../Shared/constants";
import { TErrorActions } from "./../raiseErrorAction";
import { ICookiesPromptContentDto } from "../../../Api/Models";
import { EnrichConfiguration } from "../../../Api/Request";

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

        axios(EnrichConfiguration(
        {
            method: "GET", 
            url: GET_COOKIES_PROMPT_CONTENT,
            responseType: "json"
        }))
        .then(response =>
        {
            if (response.status === 200)
            {
                return response.data === null 
                    ? RaiseError(dispatch, NULL_RESPONSE_ERROR) 
                    : dispatch({ type: RECEIVE_COOKIES_PROMPT_CONTENT, payload: response.data });
            }
            
            RaiseError(dispatch, UnexpectedStatusCode(response.status));
        })
        .catch(error =>
        {
            RaiseError(dispatch, error);
        });
    }
}