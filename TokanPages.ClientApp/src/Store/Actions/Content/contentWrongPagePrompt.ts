import { IApplicationAction, ApplicationDefault } from "../../Configuration";
import { GetContent, GET_WRONG_PAGE_PROMPT_CONTENT } from "../../../Api/Request";
import { IWrongPagePromptContentDto } from "../../../Api/Models";

export const REQUEST = "REQUEST_WRONG_PAGE_CONTENT";
export const RECEIVE = "RECEIVE_WRONG_PAGE_CONTENT";
interface IRequest { type: typeof REQUEST }
interface IReceive { type: typeof RECEIVE, payload: IWrongPagePromptContentDto }
export type TKnownActions = IRequest | IReceive;

export const ContentWrongPagePromptAction = 
{
    get: (): IApplicationAction<TKnownActions> => (dispatch, getState) =>
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