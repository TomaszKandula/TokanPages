import { ApplicationAction, ApplicationDefault } from "../../Configuration";
import { GetContent, GET_TERMS_CONTENT } from "../../../Api/Request";
import { DocumentContentDto } from "../../../Api/Models";

export const REQUEST = "REQUEST_TERMS_CONTENT";
export const RECEIVE = "RECEIVE_TERMS_CONTENT";
interface Request { type: typeof REQUEST }
interface Receive { type: typeof RECEIVE, payload: DocumentContentDto }
export type TKnownActions = Request | Receive;

export const ContentTermsAction = 
{
    get: (): ApplicationAction<TKnownActions> => (dispatch, getState) =>
    {
        const content = getState().contentTerms.content;
        const languageId = getState().applicationLanguage.id
        const isContentChanged = content !== ApplicationDefault.contentTerms.content;
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
            url: GET_TERMS_CONTENT 
        });
    }
}