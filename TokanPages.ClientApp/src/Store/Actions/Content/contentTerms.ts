import { IApplicationAction, ApplicationDefault } from "../../Configuration";
import { TERMS_URL } from "../../../Shared/constants";
import { IDocumentContentDto } from "../../../Api/Models";
import { GetContentService } from "./Services/getContentService";

export const REQUEST_TERMS_CONTENT = "REQUEST_TERMS_CONTENT";
export const RECEIVE_TERMS_CONTENT = "RECEIVE_TERMS_CONTENT";
export interface IRequestTermsContent { type: typeof REQUEST_TERMS_CONTENT }
export interface IReceiveTermsContent { type: typeof RECEIVE_TERMS_CONTENT, payload: IDocumentContentDto }
export type TKnownActions = IRequestTermsContent | IReceiveTermsContent;

export const ContentTermsAction = 
{
    get: (): IApplicationAction<TKnownActions> => (dispatch, getState) =>
    {
        const content = getState().contentTerms.content;
        const languageId = getState().applicationLanguage.id
        const isContentChanged = content !== ApplicationDefault.contentTerms.content;
        const isLanguageChanged = languageId !== content.language;

        if (isContentChanged && !isLanguageChanged) 
        {
            return;
        }

        GetContentService(
        { 
            dispatch: dispatch, 
            state: getState, 
            request: REQUEST_TERMS_CONTENT, 
            receive: RECEIVE_TERMS_CONTENT, 
            url: TERMS_URL 
        });
    }
}