import { AppThunkAction, ApplicationDefaults } from "../../Configuration";
import { GET_HEADER_CONTENT } from "../../../Shared/constants";
import { IHeaderContentDto } from "../../../Api/Models";
import { GetContent } from "./Services/getContentService";

export const REQUEST_HEADER_CONTENT = "REQUEST_HEADER_CONTENT";
export const RECEIVE_HEADER_CONTENT = "RECEIVE_HEADER_CONTENT";
export interface IRequestHeaderContent { type: typeof REQUEST_HEADER_CONTENT }
export interface IReceiveHeaderContent { type: typeof RECEIVE_HEADER_CONTENT, payload: IHeaderContentDto }
export type TKnownActions = IRequestHeaderContent | IReceiveHeaderContent;

export const ContentHeaderAction = 
{
    get: (): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        const isLanguageChanged = getState().applicationLanguage.id !== getState().contentHeader.content.language;

        if (getState().contentHeader.content !== ApplicationDefaults.contentHeader.content && !isLanguageChanged) 
        {
            return;
        }

        GetContent(
        { 
            dispatch: dispatch, 
            state: getState, 
            request: REQUEST_HEADER_CONTENT, 
            receive: RECEIVE_HEADER_CONTENT, 
            url: GET_HEADER_CONTENT 
        });
    }
}