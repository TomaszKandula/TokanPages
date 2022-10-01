import { IApplicationAction, ApplicationDefault } from "../../Configuration";
import { GET_COOKIES_PROMPT_CONTENT } from "../../../Shared/constants";
import { ICookiesPromptContentDto } from "../../../Api/Models";
import { GetContentService } from "./Services/getContentService";

export const REQUEST_COOKIES_PROMPT_CONTENT = "REQUEST_COOKIES_PROMPT_CONTENT";
export const RECEIVE_COOKIES_PROMPT_CONTENT = "RECEIVE_COOKIES_PROMPT_CONTENT";
export interface IRequestCookiesPromptContent { type: typeof REQUEST_COOKIES_PROMPT_CONTENT }
export interface IReceiveCookiesPromptContent { type: typeof RECEIVE_COOKIES_PROMPT_CONTENT, payload: ICookiesPromptContentDto }
export type TKnownActions = IRequestCookiesPromptContent | IReceiveCookiesPromptContent;

export const ContentCookiesPromptAction = 
{
    get: (): IApplicationAction<TKnownActions> => (dispatch, getState) =>
    {
        const content = getState().contentCookiesPrompt.content
        const languageId = getState().applicationLanguage.id;
        const isContentChanged = content !== ApplicationDefault.contentCookiesPrompt.content;
        const isLanguageChanged = languageId !== content.language;

        if (isContentChanged && !isLanguageChanged) 
        {
            return;
        }

        GetContentService(
        { 
            dispatch: dispatch, 
            state: getState, 
            request: REQUEST_COOKIES_PROMPT_CONTENT, 
            receive: RECEIVE_COOKIES_PROMPT_CONTENT, 
            url: GET_COOKIES_PROMPT_CONTENT 
        });
    }
}