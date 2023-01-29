import { ApplicationAction, ApplicationDefault } from "../../Configuration";
import { GetContent, GET_CONTACT_FORM_CONTENT } from "../../../Api/Request";
import { ContactFormContentDto } from "../../../Api/Models";

export const REQUEST = "REQUEST_CONTACT_FORM_CONTENT";
export const RECEIVE = "RECEIVE_CONTACT_FORM_CONTENT";
interface Request { type: typeof REQUEST }
interface Receive { type: typeof RECEIVE, payload: ContactFormContentDto }
export type TKnownActions = Request | Receive;

export const ContentContactFormAction = 
{
    get: (): ApplicationAction<TKnownActions> => (dispatch, getState) =>
    {
        const content = getState().contentContactForm.content
        const languageId = getState().applicationLanguage.id;
        const isContentChanged = content !== ApplicationDefault.contentContactForm.content;
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
            url: GET_CONTACT_FORM_CONTENT 
        });
    }
}