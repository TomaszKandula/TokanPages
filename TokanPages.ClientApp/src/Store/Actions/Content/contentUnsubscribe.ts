import { ApplicationAction, ApplicationDefault } from "../../Configuration";
import { GetContent, GET_UNSUBSCRIBE_CONTENT } from "../../../Api/Request";
import { UnsubscribeContentDto } from "../../../Api/Models";

export const REQUEST = "REQUEST_UNSUBSCRIBE_CONTENT";
export const RECEIVE = "RECEIVE_UNSUBSCRIBE_CONTENT";
interface Request { type: typeof REQUEST }
interface Receive { type: typeof RECEIVE, payload: UnsubscribeContentDto }
export type TKnownActions = Request | Receive;

export const ContentUnsubscribeAction = 
{
    get: (): ApplicationAction<TKnownActions> => (dispatch, getState) =>
    {
        const content = getState().contentUnsubscribe.content;
        const languageId = getState().applicationLanguage.id;
        const isContentChanged = content !== ApplicationDefault.contentUnsubscribe.content;
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
            url: GET_UNSUBSCRIBE_CONTENT 
        });
    }
}