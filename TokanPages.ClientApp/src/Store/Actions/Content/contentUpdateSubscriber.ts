import { ApplicationAction, ApplicationDefault } from "../../Configuration";
import { GetContent, GET_UPDATE_SUBSCRIBER_CONTENT } from "../../../Api/Request";
import { UpdateSubscriberContentDto } from "../../../Api/Models";

export const REQUEST = "REQUEST_UPDATE_SUBSCRIBER_CONTENT";
export const RECEIVE = "RECEIVE_UPDATE_SUBSCRIBER_CONTENT";
interface Request { type: typeof REQUEST }
interface Receive { type: typeof RECEIVE, payload: UpdateSubscriberContentDto }
export type TKnownActions = Request | Receive;

export const ContentUpdateSubscriberAction = 
{
    get: (): ApplicationAction<TKnownActions> => (dispatch, getState) =>
    {
        const content = getState().contentUpdateSubscriber.content;
        const languageId = getState().applicationLanguage.id;
        const isContentChanged = content !== ApplicationDefault.contentUpdateSubscriber.content;
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
            url: GET_UPDATE_SUBSCRIBER_CONTENT 
        });
    }
}