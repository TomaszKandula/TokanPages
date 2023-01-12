import { IApplicationAction, ApplicationDefault } from "../../Configuration";
import { GetContent, GET_COOKIES_PROMPT_CONTENT } from "../../../Api/Request";
import { ICookiesPromptContentDto } from "../../../Api/Models";

export const REQUEST = "REQUEST_COOKIES_PROMPT_CONTENT";
export const RECEIVE = "RECEIVE_COOKIES_PROMPT_CONTENT";
interface IRequest { type: typeof REQUEST }
interface IReceive { type: typeof RECEIVE, payload: ICookiesPromptContentDto }
export type TKnownActions = IRequest | IReceive;

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

        GetContent(
        { 
            dispatch: dispatch, 
            state: getState, 
            request: REQUEST, 
            receive: RECEIVE, 
            url: GET_COOKIES_PROMPT_CONTENT 
        });
    }
}