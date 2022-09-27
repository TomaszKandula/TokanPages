import { AppThunkAction } from "../../applicationState";
import { combinedDefaults } from "../../../Redux/combinedDefaults";
import { GET_HEADER_CONTENT } from "../../../Shared/constants";
import { TErrorActions } from "./../raiseErrorAction";
import { IHeaderContentDto } from "../../../Api/Models";
import { GetContent } from "./Services/getContentService";

export const REQUEST_HEADER_CONTENT = "REQUEST_HEADER_CONTENT";
export const RECEIVE_HEADER_CONTENT = "RECEIVE_HEADER_CONTENT";
export interface IRequestHeaderContent { type: typeof REQUEST_HEADER_CONTENT }
export interface IReceiveHeaderContent { type: typeof RECEIVE_HEADER_CONTENT, payload: IHeaderContentDto }
export type TKnownActions = IRequestHeaderContent | IReceiveHeaderContent | TErrorActions;

export const ActionCreators = 
{
    getHeaderContent: (): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        const isLanguageChanged = getState().userLanguage.id !== getState().getHeaderContent.content.language;

        if (getState().getHeaderContent.content !== combinedDefaults.getHeaderContent.content && !isLanguageChanged) 
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