import { AppThunkAction, ApplicationDefaults } from "../../Configuration";
import { TERMS_URL } from "../../../Shared/constants";
import { TErrorActions } from "../raiseErrorAction";
import { IDocumentContentDto } from "../../../Api/Models";
import { GetContent } from "./Services/getContentService";

export const REQUEST_TERMS_CONTENT = "REQUEST_TERMS_CONTENT";
export const RECEIVE_TERMS_CONTENT = "RECEIVE_TERMS_CONTENT";
export interface IRequestTermsContent { type: typeof REQUEST_TERMS_CONTENT }
export interface IReceiveTermsContent { type: typeof RECEIVE_TERMS_CONTENT, payload: IDocumentContentDto }
export type TKnownActions = IRequestTermsContent | IReceiveTermsContent | TErrorActions;

export const ActionCreators = 
{
    get: (): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        const isLanguageChanged = getState().applicationLanguage.id !== getState().contentTerms.content.language;

        if (getState().contentTerms.content !== ApplicationDefaults.contentTerms.content && !isLanguageChanged) 
        {
            return;
        }

        GetContent(
        { 
            dispatch: dispatch, 
            state: getState, 
            request: REQUEST_TERMS_CONTENT, 
            receive: RECEIVE_TERMS_CONTENT, 
            url: TERMS_URL 
        });
    }
}