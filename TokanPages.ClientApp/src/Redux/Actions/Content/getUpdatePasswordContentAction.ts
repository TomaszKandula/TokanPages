import { AppThunkAction } from "../../applicationState";
import { combinedDefaults } from "../../combinedDefaults";
import { GET_UPDATE_PASSWORD_CONTENT } from "../../../Shared/constants";
import { TErrorActions } from "../raiseErrorAction";
import { IUpdatePasswordContentDto } from "../../../Api/Models";
import { GetContent } from "./Services/getContentService";

export const REQUEST_UPDATE_PASSWORD_CONTENT = "REQUEST_UPDATE_PASSWORD_CONTENT";
export const RECEIVE_UPDATE_PASSWORD_CONTENT = "RECEIVE_UPDATE_PASSWORD_CONTENT";
export interface IRequestUpdatePasswordContent { type: typeof REQUEST_UPDATE_PASSWORD_CONTENT }
export interface IReceiveUpdatePasswordContent { type: typeof RECEIVE_UPDATE_PASSWORD_CONTENT, payload: IUpdatePasswordContentDto }
export type TKnownActions = IRequestUpdatePasswordContent | IReceiveUpdatePasswordContent | TErrorActions;

export const ActionCreators = 
{
    getUpdatePasswordContent: (): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        const isLanguageChanged = getState().userLanguage.id !== getState().getUpdatePasswordContent.content.language;

        if (getState().getUpdatePasswordContent.content !== combinedDefaults.getUpdatePasswordContent.content && !isLanguageChanged) 
        {
            return;
        }

        GetContent(
        { 
            dispatch: dispatch, 
            state: getState, 
            request: REQUEST_UPDATE_PASSWORD_CONTENT, 
            receive: RECEIVE_UPDATE_PASSWORD_CONTENT, 
            url: GET_UPDATE_PASSWORD_CONTENT 
        });
    }
}