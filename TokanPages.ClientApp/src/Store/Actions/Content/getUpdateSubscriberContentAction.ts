import { AppThunkAction, ApplicationDefaults } from "../../Configuration";
import { GET_UPDATE_SUBSCRIBER_CONTENT } from "../../../Shared/constants";
import { TErrorActions } from "../raiseErrorAction";
import { IUpdateSubscriberContentDto } from "../../../Api/Models";
import { GetContent } from "./Services/getContentService";

export const REQUEST_UPDATE_SUBSCRIBER_CONTENT = "REQUEST_UPDATE_SUBSCRIBER_CONTENT";
export const RECEIVE_UPDATE_SUBSCRIBER_CONTENT = "RECEIVE_UPDATE_SUBSCRIBER_CONTENT";
export interface IRequestUpdateSubscriberContent { type: typeof REQUEST_UPDATE_SUBSCRIBER_CONTENT }
export interface IReceiveUpdateSubscriberContent { type: typeof RECEIVE_UPDATE_SUBSCRIBER_CONTENT, payload: IUpdateSubscriberContentDto }
export type TKnownActions = IRequestUpdateSubscriberContent | IReceiveUpdateSubscriberContent | TErrorActions;

export const ActionCreators = 
{
    get: (): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        const isLanguageChanged = getState().applicationLanguage.id !== getState().contentUpdateSubscriber.content.language;

        if (getState().contentUpdateSubscriber.content !== ApplicationDefaults.contentUpdateSubscriber.content && !isLanguageChanged) 
        {
            return;
        }

        GetContent(
        { 
            dispatch: dispatch, 
            state: getState, 
            request: REQUEST_UPDATE_SUBSCRIBER_CONTENT, 
            receive: RECEIVE_UPDATE_SUBSCRIBER_CONTENT, 
            url: GET_UPDATE_SUBSCRIBER_CONTENT 
        });
    }
}