import axios from "axios";
import { AppThunkAction } from "../../applicationState";
import { combinedDefaults } from "../../../Redux/combinedDefaults";
import { GetTextStatusCode } from "../../../Shared/Services/Utilities";
import { RaiseError } from "../../../Shared/Services/ErrorServices";
import { GET_COOKIES_PROMPT_CONTENT, NULL_RESPONSE_ERROR } from "../../../Shared/constants";
import { TErrorActions } from "./../raiseErrorAction";
import { ICookiesPromptContentDto } from "../../../Api/Models";
import { EnrichConfiguration } from "../../../Api/Request";
import { GetUserLanguageFromStore } from "../../../Shared/Services/languageService";

export const REQUEST_COOKIES_PROMPT_CONTENT = "REQUEST_COOKIES_PROMPT_CONTENT";
export const RECEIVE_COOKIES_PROMPT_CONTENT = "RECEIVE_COOKIES_PROMPT_CONTENT";
export interface IRequestCookiesPromptContent { type: typeof REQUEST_COOKIES_PROMPT_CONTENT }
export interface IReceiveCookiesPromptContent { type: typeof RECEIVE_COOKIES_PROMPT_CONTENT, payload: ICookiesPromptContentDto }
export type TKnownActions = IRequestCookiesPromptContent | IReceiveCookiesPromptContent | TErrorActions;

export const ActionCreators = 
{
    getCookiesPromptContent: (): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        const isLanguageChanged = getState().userLanguage.id !== getState().getCookiesPromptContent.content.language;

        if (getState().getCookiesPromptContent.content !== combinedDefaults.getCookiesPromptContent.content && !isLanguageChanged) 
            return;

        dispatch({ type: REQUEST_COOKIES_PROMPT_CONTENT });

        const language = GetUserLanguageFromStore();
        const queryParam = language === "" ? "" : `&language=${language}`;

        axios(EnrichConfiguration(
        {
            method: "GET", 
            url: `${GET_COOKIES_PROMPT_CONTENT}${queryParam}`,
            responseType: "json"
        }))
        .then(response =>
        {
            if (response.status === 200)
            {
                return response.data === null 
                    ? RaiseError({ dispatch, errorObject: NULL_RESPONSE_ERROR }) 
                    : dispatch({ type: RECEIVE_COOKIES_PROMPT_CONTENT, payload: response.data });
            }
            
            RaiseError({ dispatch, errorObject: GetTextStatusCode({ statusCode: response.status }) });
        })
        .catch(error =>
        {
            RaiseError({ dispatch: dispatch, errorObject: error });
        });
    }
}