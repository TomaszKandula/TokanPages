import { ApplicationAction, ApplicationDefault } from "../../Configuration";
import { GetContent, GET_WRONG_PAGE_PROMPT_CONTENT } from "../../../Api/Request";
import { WrongPagePromptContentDto } from "../../../Api/Models";

export const REQUEST = "REQUEST_WRONG_PAGE_CONTENT";
export const RECEIVE = "RECEIVE_WRONG_PAGE_CONTENT";
interface Request { type: typeof REQUEST }
interface Receive { type: typeof RECEIVE, payload: WrongPagePromptContentDto }
export type TKnownActions = Request | Receive;

export const ContentWrongPagePromptAction = 
{
    get: (): ApplicationAction<TKnownActions> => (dispatch, getState) =>
    {
        const content = getState().contentWrongPagePrompt.content;
        const languageId = getState().applicationLanguage.id;
        const isContentChanged = content !== ApplicationDefault.contentWrongPagePrompt.content;
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
            url: GET_WRONG_PAGE_PROMPT_CONTENT 
        });
    }
}