import { AppThunkAction, ApplicationDefaults } from "../../Configuration";
import { GET_NEWSLETTER_CONTENT } from "../../../Shared/constants";
import { INewsletterContentDto } from "../../../Api/Models";
import { GetContent } from "./Services/getContentService";

export const REQUEST_NEWSLETTER_CONTENT = "REQUEST_NEWSLETTER_CONTENT";
export const RECEIVE_NEWSLETTER_CONTENT = "RECEIVE_NEWSLETTER_CONTENT";
export interface IRequestNewsletterContent { type: typeof REQUEST_NEWSLETTER_CONTENT }
export interface IReceiveNewsletterContent { type: typeof RECEIVE_NEWSLETTER_CONTENT, payload: INewsletterContentDto }
export type TKnownActions = IRequestNewsletterContent | IReceiveNewsletterContent;

export const ContentNewsletterAction = 
{
    get: (): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        const isLanguageChanged = getState().applicationLanguage.id !== getState().contentNewsletter.content.language;

        if (getState().contentNewsletter.content !== ApplicationDefaults.contentNewsletter.content && !isLanguageChanged) 
        {
            return;
        }

        GetContent(
        { 
            dispatch: dispatch, 
            state: getState, 
            request: REQUEST_NEWSLETTER_CONTENT, 
            receive: RECEIVE_NEWSLETTER_CONTENT, 
            url: GET_NEWSLETTER_CONTENT 
        });
    }
}