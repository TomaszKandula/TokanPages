import { AppThunkAction, ApplicationDefaults } from "../../Configuration";
import { GET_ACCOUNT_CONTENT } from "../../../Shared/constants";
import { IAccountContentDto } from "../../../Api/Models";
import { GetContent } from "./Services/getContentService";

export const REQUEST_ACCOUNT_CONTENT = "REQUEST_ACCOUNT_CONTENT";
export const RECEIVE_ACCOUNT_CONTENT = "RECEIVE_ACCOUNT_CONTENT";
export interface IRequestAccountContent { type: typeof REQUEST_ACCOUNT_CONTENT }
export interface IReceiveAccountContent { type: typeof RECEIVE_ACCOUNT_CONTENT, payload: IAccountContentDto }
export type TKnownActions = IRequestAccountContent | IReceiveAccountContent;

export const ContentAccountAction = 
{
    get: (): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        const isLanguageChanged = getState().applicationLanguage.id !== getState().contentAccount.content.language;

        if (getState().contentAccount.content !== ApplicationDefaults.contentAccount.content && !isLanguageChanged) 
        {
            return;
        }

        GetContent(
        { 
            dispatch: dispatch, 
            state: getState, 
            request: REQUEST_ACCOUNT_CONTENT, 
            receive: RECEIVE_ACCOUNT_CONTENT, 
            url: GET_ACCOUNT_CONTENT 
        });
    }
}