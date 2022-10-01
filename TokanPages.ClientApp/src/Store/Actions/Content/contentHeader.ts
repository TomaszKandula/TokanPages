import { IApplicationAction, ApplicationDefault } from "../../Configuration";
import { GET_HEADER_CONTENT } from "../../../Shared/constants";
import { IHeaderContentDto } from "../../../Api/Models";
import { GetContentService } from "./Services/getContentService";

export const REQUEST_HEADER_CONTENT = "REQUEST_HEADER_CONTENT";
export const RECEIVE_HEADER_CONTENT = "RECEIVE_HEADER_CONTENT";
export interface IRequestHeaderContent { type: typeof REQUEST_HEADER_CONTENT }
export interface IReceiveHeaderContent { type: typeof RECEIVE_HEADER_CONTENT, payload: IHeaderContentDto }
export type TKnownActions = IRequestHeaderContent | IReceiveHeaderContent;

export const ContentHeaderAction = 
{
    get: (): IApplicationAction<TKnownActions> => (dispatch, getState) =>
    {
        const content = getState().contentHeader.content;
        const languageId = getState().applicationLanguage.id;
        const isContentChanged = content !== ApplicationDefault.contentHeader.content;
        const isLanguageChanged = languageId !== content.language;

        if (isContentChanged && !isLanguageChanged) 
        {
            return;
        }

        GetContentService(
        { 
            dispatch: dispatch, 
            state: getState, 
            request: REQUEST_HEADER_CONTENT, 
            receive: RECEIVE_HEADER_CONTENT, 
            url: GET_HEADER_CONTENT 
        });
    }
}