import { ApplicationAction, ApplicationDefault } from "../../Configuration";
import { GetContent, GET_NEWSLETTER_CONTENT } from "../../../Api/Request";
import { NewsletterContentDto } from "../../../Api/Models";

export const REQUEST = "REQUEST_NEWSLETTER_CONTENT";
export const RECEIVE = "RECEIVE_NEWSLETTER_CONTENT";
interface Request { type: typeof REQUEST }
interface Receive { type: typeof RECEIVE, payload: NewsletterContentDto }
export type TKnownActions = Request | Receive;

export const ContentNewsletterAction = 
{
    get: (): ApplicationAction<TKnownActions> => (dispatch, getState) =>
    {
        const content = getState().contentNewsletter.content;
        const languageId = getState().applicationLanguage.id;
        const isContentChanged = content !== ApplicationDefault.contentNewsletter.content;
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
            url: GET_NEWSLETTER_CONTENT 
        });
    }
}