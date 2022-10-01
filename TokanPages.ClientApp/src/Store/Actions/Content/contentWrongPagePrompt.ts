import { IApplicationAction, ApplicationDefault } from "../../Configuration";
import { GET_WRONG_PAGE_PROMPT_CONTENT } from "../../../Shared/constants";
import { IWrongPagePromptContentDto } from "../../../Api/Models";
import { GetContent } from "./Services/getContentService";

export const REQUEST_WRONG_PAGE_CONTENT = "REQUEST_WRONG_PAGE_CONTENT";
export const RECEIVE_WRONG_PAGE_CONTENT = "RECEIVE_WRONG_PAGE_CONTENT";
export interface IRequestWrongPageContent { type: typeof REQUEST_WRONG_PAGE_CONTENT }
export interface IReceiveWrongPageContent { type: typeof RECEIVE_WRONG_PAGE_CONTENT, payload: IWrongPagePromptContentDto }
export type TKnownActions = IRequestWrongPageContent | IReceiveWrongPageContent;

export const ContentWrongPagePromptAction = 
{
    get: (): IApplicationAction<TKnownActions> => (dispatch, getState) =>
    {
        const isLanguageChanged = getState().applicationLanguage.id !== getState().contentWrongPagePrompt.content.language;

        if (getState().contentWrongPagePrompt.content !== ApplicationDefault.contentWrongPagePrompt.content && !isLanguageChanged)
        {
            return;
        }

        GetContent(
        { 
            dispatch: dispatch, 
            state: getState, 
            request: REQUEST_WRONG_PAGE_CONTENT, 
            receive: RECEIVE_WRONG_PAGE_CONTENT, 
            url: GET_WRONG_PAGE_PROMPT_CONTENT 
        });
    }
}