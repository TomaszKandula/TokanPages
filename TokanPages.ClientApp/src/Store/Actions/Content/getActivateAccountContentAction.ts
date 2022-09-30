import { AppThunkAction, ApplicationDefaults } from "../../Configuration";
import { GET_ACTIVATE_ACCOUNT_CONTENT } from "../../../Shared/constants";
import { TErrorActions } from "../raiseErrorAction";
import { IActivateAccountContentDto } from "../../../Api/Models";
import { GetContent } from "./Services/getContentService";

export const REQUEST_ACTIVATE_ACCOUNT_CONTENT = "REQUEST_ACTIVATE_ACCOUNT_CONTENT";
export const RECEIVE_ACTIVATE_ACCOUNT_CONTENT = "RECEIVE_ACTIVATE_ACCOUNT_CONTENT";
export interface IRequestActivateAccountContent { type: typeof REQUEST_ACTIVATE_ACCOUNT_CONTENT }
export interface IReceiveActivateAccountContent { type: typeof RECEIVE_ACTIVATE_ACCOUNT_CONTENT, payload: IActivateAccountContentDto }
export type TKnownActions = IRequestActivateAccountContent | IReceiveActivateAccountContent | TErrorActions;

export const ActionCreators = 
{
    get: (): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        const isLanguageChanged = getState().userLanguage.id !== getState().getActivateAccountContent.content.language;

        if (getState().getActivateAccountContent.content !== ApplicationDefaults.getActivateAccountContent.content && !isLanguageChanged) 
        {
            return;
        }

        GetContent(
        { 
            dispatch: dispatch, 
            state: getState, 
            request: REQUEST_ACTIVATE_ACCOUNT_CONTENT, 
            receive: RECEIVE_ACTIVATE_ACCOUNT_CONTENT, 
            url: GET_ACTIVATE_ACCOUNT_CONTENT 
        });
    }
}