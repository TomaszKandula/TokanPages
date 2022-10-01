import { AppThunkAction, ApplicationDefaults } from "../../Configuration";
import { GET_UNSUBSCRIBE_CONTENT } from "../../../Shared/constants";
import { TErrorActions } from "../raiseErrorAction";
import { IUnsubscribeContentDto } from "../../../Api/Models";
import { GetContent } from "./Services/getContentService";

export const REQUEST_UNSUBSCRIBE_CONTENT = "REQUEST_UNSUBSCRIBE_CONTENT";
export const RECEIVE_UNSUBSCRIBE_CONTENT = "RECEIVE_UNSUBSCRIBE_CONTENT";
export interface IRequestUnsubscribeContent { type: typeof REQUEST_UNSUBSCRIBE_CONTENT }
export interface IReceiveUnsubscribeContent { type: typeof RECEIVE_UNSUBSCRIBE_CONTENT, payload: IUnsubscribeContentDto }
export type TKnownActions = IRequestUnsubscribeContent | IReceiveUnsubscribeContent | TErrorActions;

export const ActionCreators = 
{
    get: (): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        const isLanguageChanged = getState().applicationLanguage.id !== getState().contentUnsubscribe.content.language;

        if (getState().contentUnsubscribe.content !== ApplicationDefaults.contentUnsubscribe.content && !isLanguageChanged) 
        {
            return;
        }

        GetContent(
        { 
            dispatch: dispatch, 
            state: getState, 
            request: REQUEST_UNSUBSCRIBE_CONTENT, 
            receive: RECEIVE_UNSUBSCRIBE_CONTENT, 
            url: GET_UNSUBSCRIBE_CONTENT 
        });
    }
}