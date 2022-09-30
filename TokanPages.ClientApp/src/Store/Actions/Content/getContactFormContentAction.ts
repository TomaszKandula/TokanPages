import { AppThunkAction, ApplicationDefaults } from "../../Configuration";
import { GET_CONTACT_FORM_CONTENT } from "../../../Shared/constants";
import { TErrorActions } from "../raiseErrorAction";
import { IContactFormContentDto } from "../../../Api/Models";
import { GetContent } from "./Services/getContentService";

export const REQUEST_CONTACT_FORM_CONTENT = "REQUEST_CONTACT_FORM_CONTENT";
export const RECEIVE_CONTACT_FORM_CONTENT = "RECEIVE_CONTACT_FORM_CONTENT";
export interface IRequestContactFormContent { type: typeof REQUEST_CONTACT_FORM_CONTENT }
export interface IReceiveContactFormContent { type: typeof RECEIVE_CONTACT_FORM_CONTENT, payload: IContactFormContentDto }
export type TKnownActions = IRequestContactFormContent | IReceiveContactFormContent | TErrorActions;

export const ActionCreators = 
{
    getContactFormContent: (): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        const isLanguageChanged = getState().userLanguage.id !== getState().getContactFormContent.content.language;

        if (getState().getContactFormContent.content !== ApplicationDefaults.getContactFormContent.content && !isLanguageChanged) 
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