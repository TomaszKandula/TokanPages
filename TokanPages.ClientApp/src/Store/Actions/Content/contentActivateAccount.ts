import { IApplicationAction, ApplicationDefault } from "../../Configuration";
import { GET_ACTIVATE_ACCOUNT_CONTENT } from "../../../Shared/constants";
import { IActivateAccountContentDto } from "../../../Api/Models";
import { GetContentService } from "./Services/getContentService";

export const REQUEST_ACTIVATE_ACCOUNT_CONTENT = "REQUEST_ACTIVATE_ACCOUNT_CONTENT";
export const RECEIVE_ACTIVATE_ACCOUNT_CONTENT = "RECEIVE_ACTIVATE_ACCOUNT_CONTENT";
export interface IRequestActivateAccountContent { type: typeof REQUEST_ACTIVATE_ACCOUNT_CONTENT }
export interface IReceiveActivateAccountContent { type: typeof RECEIVE_ACTIVATE_ACCOUNT_CONTENT, payload: IActivateAccountContentDto }
export type TKnownActions = IRequestActivateAccountContent | IReceiveActivateAccountContent;

export const ContentActivateAccountAction = 
{
    get: (): IApplicationAction<TKnownActions> => (dispatch, getState) =>
    {
        const content = getState().contentActivateAccount.content;
        const languageId = getState().applicationLanguage.id;
        const isContentChanged = content !== ApplicationDefault.contentActivateAccount.content;
        const isLanguageChanged = languageId !== content.language;

        if (isContentChanged && !isLanguageChanged) 
        {
            return;
        }

        GetContentService(
        { 
            dispatch: dispatch, 
            state: getState, 
            request: REQUEST_ACTIVATE_ACCOUNT_CONTENT, 
            receive: RECEIVE_ACTIVATE_ACCOUNT_CONTENT, 
            url: GET_ACTIVATE_ACCOUNT_CONTENT 
        });
    }
}