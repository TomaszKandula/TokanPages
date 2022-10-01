import { IApplicationAction, ApplicationDefault } from "../../Configuration";
import { GET_NEWSLETTER_CONTENT } from "../../../Shared/constants";
import { INewsletterContentDto } from "../../../Api/Models";
import { GetContentService } from "./Services/getContentService";

export const REQUEST_NEWSLETTER_CONTENT = "REQUEST_NEWSLETTER_CONTENT";
export const RECEIVE_NEWSLETTER_CONTENT = "RECEIVE_NEWSLETTER_CONTENT";
export interface IRequestNewsletterContent { type: typeof REQUEST_NEWSLETTER_CONTENT }
export interface IReceiveNewsletterContent { type: typeof RECEIVE_NEWSLETTER_CONTENT, payload: INewsletterContentDto }
export type TKnownActions = IRequestNewsletterContent | IReceiveNewsletterContent;

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

        GetContentService(
        { 
            dispatch: dispatch, 
            state: getState, 
            request: REQUEST_NEWSLETTER_CONTENT, 
            receive: RECEIVE_NEWSLETTER_CONTENT, 
            url: GET_NEWSLETTER_CONTENT 
        });
    }
}