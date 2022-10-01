import { IApplicationAction, ApplicationDefault } from "../../Configuration";
import { GET_ACCOUNT_CONTENT } from "../../../Shared/constants";
import { IAccountContentDto } from "../../../Api/Models";
import { GetContentService } from "./Services/getContentService";

export const REQUEST_ACCOUNT_CONTENT = "REQUEST_ACCOUNT_CONTENT";
export const RECEIVE_ACCOUNT_CONTENT = "RECEIVE_ACCOUNT_CONTENT";
export interface IRequestAccountContent { type: typeof REQUEST_ACCOUNT_CONTENT }
export interface IReceiveAccountContent { type: typeof RECEIVE_ACCOUNT_CONTENT, payload: IAccountContentDto }
export type TKnownActions = IRequestAccountContent | IReceiveAccountContent;

export const ContentAccountAction = 
{
    get: (): IApplicationAction<TKnownActions> => (dispatch, getState) =>
    {
        const content = getState().contentAccount.content;
        const languageId = getState().applicationLanguage.id;
        const isContentChanged = content !== ApplicationDefault.contentAccount.content;
        const isLanguageChanged = languageId !== content.language;

        if (isContentChanged && !isLanguageChanged) 
        {
            return;
        }

        GetContentService(
        { 
            dispatch: dispatch, 
            state: getState, 
            request: REQUEST_ACCOUNT_CONTENT, 
            receive: RECEIVE_ACCOUNT_CONTENT, 
            url: GET_ACCOUNT_CONTENT 
        });
    }
}