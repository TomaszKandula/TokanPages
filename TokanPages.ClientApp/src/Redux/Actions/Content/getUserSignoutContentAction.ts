import { AppThunkAction } from "../../applicationState";
import { combinedDefaults } from "../../combinedDefaults";
import { GET_SIGNOUT_CONTENT } from "../../../Shared/constants";
import { TErrorActions } from "./../raiseErrorAction";
import { IUserSignoutContentDto } from "../../../Api/Models";
import { GetContent } from "./Services/getContentService";

export const REQUEST_USER_SIGNOUT_CONTENT = "REQUEST_USER_SIGNOUT_CONTENT";
export const RECEIVE_USER_SIGNOUT_CONTENT = "RECEIVE_USER_SIGNOUT_CONTENT";
export interface IRequestSignoutFormContent { type: typeof REQUEST_USER_SIGNOUT_CONTENT }
export interface IReceiveSignoutFormContent { type: typeof RECEIVE_USER_SIGNOUT_CONTENT, payload: IUserSignoutContentDto }
export type TKnownActions = IRequestSignoutFormContent | IReceiveSignoutFormContent | TErrorActions;

export const ActionCreators = 
{
    getUserSignoutContent: (): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        const isLanguageChanged = getState().userLanguage.id !== getState().getUserSignoutContent.content.language;

        if (getState().getUserSignoutContent.content !== combinedDefaults.getUserSignoutContent.content && !isLanguageChanged) 
        {
            return;
        }

        GetContent(
        { 
            dispatch: dispatch, 
            state: getState, 
            request: REQUEST_USER_SIGNOUT_CONTENT, 
            receive: RECEIVE_USER_SIGNOUT_CONTENT, 
            url: GET_SIGNOUT_CONTENT 
        });
    }
}