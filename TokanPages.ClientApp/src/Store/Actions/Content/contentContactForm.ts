import { IApplicationAction, ApplicationDefault } from "../../Configuration";
import { GET_CONTACT_FORM_CONTENT } from "../../../Shared/constants";
import { IContactFormContentDto } from "../../../Api/Models";
import { GetContentService } from "./Services/getContentService";

export const REQUEST_CONTACT_FORM_CONTENT = "REQUEST_CONTACT_FORM_CONTENT";
export const RECEIVE_CONTACT_FORM_CONTENT = "RECEIVE_CONTACT_FORM_CONTENT";
export interface IRequestContactFormContent { type: typeof REQUEST_CONTACT_FORM_CONTENT }
export interface IReceiveContactFormContent { type: typeof RECEIVE_CONTACT_FORM_CONTENT, payload: IContactFormContentDto }
export type TKnownActions = IRequestContactFormContent | IReceiveContactFormContent;

export const ContentContactFormAction = 
{
    get: (): IApplicationAction<TKnownActions> => (dispatch, getState) =>
    {
        const content = getState().contentContactForm.content
        const languageId = getState().applicationLanguage.id;
        const isContentChanged = content !== ApplicationDefault.contentContactForm.content;
        const isLanguageChanged = languageId !== content.language;

        if (isContentChanged && !isLanguageChanged) 
        {
            return;
        }

        GetContentService(
        { 
            dispatch: dispatch, 
            state: getState, 
            request: REQUEST_CONTACT_FORM_CONTENT, 
            receive: RECEIVE_CONTACT_FORM_CONTENT, 
            url: GET_CONTACT_FORM_CONTENT 
        });
    }
}