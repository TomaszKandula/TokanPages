import { IApplicationAction, ApplicationDefault } from "../../Configuration";
import { GET_UPDATE_SUBSCRIBER_CONTENT } from "../../../Shared/constants";
import { IUpdateSubscriberContentDto } from "../../../Api/Models";
import { GetContentService } from "./Services/getContentService";

export const REQUEST_UPDATE_SUBSCRIBER_CONTENT = "REQUEST_UPDATE_SUBSCRIBER_CONTENT";
export const RECEIVE_UPDATE_SUBSCRIBER_CONTENT = "RECEIVE_UPDATE_SUBSCRIBER_CONTENT";
export interface IRequestUpdateSubscriberContent { type: typeof REQUEST_UPDATE_SUBSCRIBER_CONTENT }
export interface IReceiveUpdateSubscriberContent { type: typeof RECEIVE_UPDATE_SUBSCRIBER_CONTENT, payload: IUpdateSubscriberContentDto }
export type TKnownActions = IRequestUpdateSubscriberContent | IReceiveUpdateSubscriberContent;

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

        GetContentService(
        { 
            dispatch: dispatch, 
            state: getState, 
            request: REQUEST_UPDATE_SUBSCRIBER_CONTENT, 
            receive: RECEIVE_UPDATE_SUBSCRIBER_CONTENT, 
            url: GET_UPDATE_SUBSCRIBER_CONTENT 
        });
    }
}