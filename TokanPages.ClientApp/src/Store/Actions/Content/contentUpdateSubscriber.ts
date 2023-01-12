import { IApplicationAction, ApplicationDefault } from "../../Configuration";
import { GetContent, GET_UPDATE_SUBSCRIBER_CONTENT } from "../../../Api/Request";
import { IUpdateSubscriberContentDto } from "../../../Api/Models";

export const REQUEST = "REQUEST_UPDATE_SUBSCRIBER_CONTENT";
export const RECEIVE = "RECEIVE_UPDATE_SUBSCRIBER_CONTENT";
interface IRequest { type: typeof REQUEST }
interface IReceive { type: typeof RECEIVE, payload: IUpdateSubscriberContentDto }
export type TKnownActions = IRequest | IReceive;

export const ContentUpdateSubscriberAction = 
{
    get: (): IApplicationAction<TKnownActions> => (dispatch, getState) =>
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