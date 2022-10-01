import { AppThunkAction, ApplicationDefaults } from "../../Configuration";
import { GET_FOOTER_CONTENT } from "../../../Shared/constants";
import { TErrorActions } from "../raiseErrorAction";
import { IFooterContentDto } from "../../../Api/Models";
import { GetContent } from "./Services/getContentService";

export const REQUEST_FOOTER_CONTENT = "REQUEST_FOOTER_CONTENT";
export const RECEIVE_FOOTER_CONTENT = "RECEIVE_FOOTER_CONTENT";
export interface IRequestFooterContent { type: typeof REQUEST_FOOTER_CONTENT }
export interface IReceiveFooterContent { type: typeof RECEIVE_FOOTER_CONTENT, payload: IFooterContentDto }
export type TKnownActions = IRequestFooterContent | IReceiveFooterContent | TErrorActions;

export const ActionCreators = 
{
    get: (): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        const isLanguageChanged = getState().applicationLanguage.id !== getState().contentFooter.content.language;

        if (getState().contentFooter.content !== ApplicationDefaults.contentFooter.content && !isLanguageChanged) 
        {
            return;
        }

        GetContent(
        { 
            dispatch: dispatch, 
            state: getState, 
            request: REQUEST_FOOTER_CONTENT, 
            receive: RECEIVE_FOOTER_CONTENT, 
            url: GET_FOOTER_CONTENT 
        });
    }
}