import { IApplicationAction, ApplicationDefault } from "../../Configuration";
import { GetContent, GET_TERMS_CONTENT } from "../../../Api/Request";
import { IDocumentContentDto } from "../../../Api/Models";

export const REQUEST = "REQUEST_TERMS_CONTENT";
export const RECEIVE = "RECEIVE_TERMS_CONTENT";
interface IRequest { type: typeof REQUEST }
interface IReceive { type: typeof RECEIVE, payload: IDocumentContentDto }
export type TKnownActions = IRequest | IReceive;

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

        GetContent(
        { 
            dispatch: dispatch, 
            state: getState, 
            request: REQUEST, 
            receive: RECEIVE, 
            url: GET_TERMS_CONTENT 
        });
    }
}