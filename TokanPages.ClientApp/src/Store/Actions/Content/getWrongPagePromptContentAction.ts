import { AppThunkAction, ApplicationDefaults } from "../../Configuration";
import { GET_WRONG_PAGE_PROMPT_CONTENT } from "../../../Shared/constants";
import { TErrorActions } from "../applicationError";
import { IWrongPagePromptContentDto } from "../../../Api/Models";
import { GetContent } from "./Services/getContentService";

export const REQUEST_WRONG_PAGE_CONTENT = "REQUEST_WRONG_PAGE_CONTENT";
export const RECEIVE_WRONG_PAGE_CONTENT = "RECEIVE_WRONG_PAGE_CONTENT";
export interface IRequestWrongPageContent { type: typeof REQUEST_WRONG_PAGE_CONTENT }
export interface IReceiveWrongPageContent { type: typeof RECEIVE_WRONG_PAGE_CONTENT, payload: IWrongPagePromptContentDto }
export type TKnownActions = IRequestWrongPageContent | IReceiveWrongPageContent | TErrorActions;

export const ActionCreators = 
{
    get: (): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        const isLanguageChanged = getState().applicationLanguage.id !== getState().contentWrongPagePrompt.content.language;

        if (getState().contentWrongPagePrompt.content !== ApplicationDefaults.contentWrongPagePrompt.content && !isLanguageChanged)
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