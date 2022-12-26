import { IApplicationAction, ApplicationDefault } from "../../Configuration";
import { GET_UNSUBSCRIBE_CONTENT } from "../../../Api/Request";
import { IUnsubscribeContentDto } from "../../../Api/Models";
import { GetContentService } from "./Services/getContentService";

export const REQUEST = "REQUEST_UNSUBSCRIBE_CONTENT";
export const RECEIVE = "RECEIVE_UNSUBSCRIBE_CONTENT";
interface IRequest { type: typeof REQUEST }
interface IReceive { type: typeof RECEIVE, payload: IUnsubscribeContentDto }
export type TKnownActions = IRequest | IReceive;

export const ContentUnsubscribeAction = 
{
    get: (): IApplicationAction<TKnownActions> => (dispatch, getState) =>
    {
        const content = getState().contentUnsubscribe.content;
        const languageId = getState().applicationLanguage.id;
        const isContentChanged = content !== ApplicationDefault.contentUnsubscribe.content;
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
            url: GET_UNSUBSCRIBE_CONTENT 
        });
    }
}