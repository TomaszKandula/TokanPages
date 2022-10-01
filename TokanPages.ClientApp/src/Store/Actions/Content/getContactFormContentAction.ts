import { AppThunkAction, ApplicationDefaults } from "../../Configuration";
import { GET_CONTACT_FORM_CONTENT } from "../../../Shared/constants";
import { IContactFormContentDto } from "../../../Api/Models";
import { GetContent } from "./Services/getContentService";

export const REQUEST_CONTACT_FORM_CONTENT = "REQUEST_CONTACT_FORM_CONTENT";
export const RECEIVE_CONTACT_FORM_CONTENT = "RECEIVE_CONTACT_FORM_CONTENT";
export interface IRequestContactFormContent { type: typeof REQUEST_CONTACT_FORM_CONTENT }
export interface IReceiveContactFormContent { type: typeof RECEIVE_CONTACT_FORM_CONTENT, payload: IContactFormContentDto }
export type TKnownActions = IRequestContactFormContent | IReceiveContactFormContent;

export const ContentContactFormAction = 
{
    get: (): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        const isLanguageChanged = getState().applicationLanguage.id !== getState().contentContactForm.content.language;

        if (getState().contentContactForm.content !== ApplicationDefaults.contentContactForm.content && !isLanguageChanged) 
        {
            return;
        }

        GetContent(
        { 
            dispatch: dispatch, 
            state: getState, 
            request: REQUEST_CONTACT_FORM_CONTENT, 
            receive: RECEIVE_CONTACT_FORM_CONTENT, 
            url: GET_CONTACT_FORM_CONTENT 
        });
    }
}