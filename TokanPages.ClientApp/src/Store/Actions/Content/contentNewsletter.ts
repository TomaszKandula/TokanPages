import { IApplicationAction, ApplicationDefault } from "../../Configuration";
import { GetContent, GET_NEWSLETTER_CONTENT } from "../../../Api/Request";
import { INewsletterContentDto } from "../../../Api/Models";

export const REQUEST = "REQUEST_NEWSLETTER_CONTENT";
export const RECEIVE = "RECEIVE_NEWSLETTER_CONTENT";
interface IRequest { type: typeof REQUEST }
interface IReceive { type: typeof RECEIVE, payload: INewsletterContentDto }
export type TKnownActions = IRequest | IReceive;

export const ContentNewsletterAction = 
{
    get: (): IApplicationAction<TKnownActions> => (dispatch, getState) =>
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