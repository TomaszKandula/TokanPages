import { IApplicationAction, ApplicationDefaults } from "../../Configuration";
import { GET_COOKIES_PROMPT_CONTENT } from "../../../Shared/constants";
import { ICookiesPromptContentDto } from "../../../Api/Models";
import { GetContent } from "./Services/getContentService";

export const REQUEST_COOKIES_PROMPT_CONTENT = "REQUEST_COOKIES_PROMPT_CONTENT";
export const RECEIVE_COOKIES_PROMPT_CONTENT = "RECEIVE_COOKIES_PROMPT_CONTENT";
export interface IRequestCookiesPromptContent { type: typeof REQUEST_COOKIES_PROMPT_CONTENT }
export interface IReceiveCookiesPromptContent { type: typeof RECEIVE_COOKIES_PROMPT_CONTENT, payload: ICookiesPromptContentDto }
export type TKnownActions = IRequestCookiesPromptContent | IReceiveCookiesPromptContent;

export const ContentCookiesPromptAction = 
{
    get: (): IApplicationAction<TKnownActions> => (dispatch, getState) =>
    {
        const isLanguageChanged = getState().applicationLanguage.id !== getState().contentCookiesPrompt.content.language;

        if (getState().contentCookiesPrompt.content !== ApplicationDefaults.contentCookiesPrompt.content && !isLanguageChanged) 
        {
            return;
        }

        GetContent(
        { 
            dispatch: dispatch, 
            state: getState, 
            request: REQUEST_COOKIES_PROMPT_CONTENT, 
            receive: RECEIVE_COOKIES_PROMPT_CONTENT, 
            url: GET_COOKIES_PROMPT_CONTENT 
        });
    }
}