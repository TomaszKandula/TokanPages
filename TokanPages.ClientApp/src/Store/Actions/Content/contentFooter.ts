import { IApplicationAction, ApplicationDefault } from "../../Configuration";
import { GET_FOOTER_CONTENT } from "../../../Shared/constants";
import { IFooterContentDto } from "../../../Api/Models";
import { GetContentService } from "./Services/getContentService";

export const REQUEST_FOOTER_CONTENT = "REQUEST_FOOTER_CONTENT";
export const RECEIVE_FOOTER_CONTENT = "RECEIVE_FOOTER_CONTENT";
export interface IRequestFooterContent { type: typeof REQUEST_FOOTER_CONTENT }
export interface IReceiveFooterContent { type: typeof RECEIVE_FOOTER_CONTENT, payload: IFooterContentDto }
export type TKnownActions = IRequestFooterContent | IReceiveFooterContent;

export const ContentFooterAction = 
{
    get: (): IApplicationAction<TKnownActions> => (dispatch, getState) =>
    {
        const content = getState().contentFooter.content;
        const languageId = getState().applicationLanguage.id;
        const isContentChanged = content !== ApplicationDefault.contentFooter.content;
        const isLanguageChanged = languageId !== content.language;

        if (isContentChanged && !isLanguageChanged) 
        {
            return;
        }

        GetContentService(
        { 
            dispatch: dispatch, 
            state: getState, 
            request: REQUEST_FOOTER_CONTENT, 
            receive: RECEIVE_FOOTER_CONTENT, 
            url: GET_FOOTER_CONTENT 
        });
    }
}