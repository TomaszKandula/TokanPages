import { IApplicationAction, ApplicationDefault } from "../../Configuration";
import { GET_CONTACT_FORM_CONTENT } from "../../../Api/Request";
import { IContactFormContentDto } from "../../../Api/Models";
import { GetContentService } from "./Services/getContentService";

export const REQUEST = "REQUEST_CONTACT_FORM_CONTENT";
export const RECEIVE = "RECEIVE_CONTACT_FORM_CONTENT";
interface IRequest { type: typeof REQUEST }
interface IReceive { type: typeof RECEIVE, payload: IContactFormContentDto }
export type TKnownActions = IRequest | IReceive;

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
            request: REQUEST, 
            receive: RECEIVE, 
            url: GET_CONTACT_FORM_CONTENT 
        });
    }
}