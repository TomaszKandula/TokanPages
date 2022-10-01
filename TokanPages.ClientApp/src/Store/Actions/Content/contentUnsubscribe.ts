import { IApplicationAction, ApplicationDefault } from "../../Configuration";
import { GET_UNSUBSCRIBE_CONTENT } from "../../../Shared/constants";
import { IUnsubscribeContentDto } from "../../../Api/Models";
import { GetContentService } from "./Services/getContentService";

export const REQUEST_UNSUBSCRIBE_CONTENT = "REQUEST_UNSUBSCRIBE_CONTENT";
export const RECEIVE_UNSUBSCRIBE_CONTENT = "RECEIVE_UNSUBSCRIBE_CONTENT";
export interface IRequestUnsubscribeContent { type: typeof REQUEST_UNSUBSCRIBE_CONTENT }
export interface IReceiveUnsubscribeContent { type: typeof RECEIVE_UNSUBSCRIBE_CONTENT, payload: IUnsubscribeContentDto }
export type TKnownActions = IRequestUnsubscribeContent | IReceiveUnsubscribeContent;

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
            request: REQUEST_UNSUBSCRIBE_CONTENT, 
            receive: RECEIVE_UNSUBSCRIBE_CONTENT, 
            url: GET_UNSUBSCRIBE_CONTENT 
        });
    }
}