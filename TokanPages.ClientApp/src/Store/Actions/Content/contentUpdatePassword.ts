import { IApplicationAction, ApplicationDefault } from "../../Configuration";
import { GET_UPDATE_PASSWORD_CONTENT } from "../../../Shared/constants";
import { IUpdatePasswordContentDto } from "../../../Api/Models";
import { GetContentService } from "./Services/getContentService";

export const REQUEST_UPDATE_PASSWORD_CONTENT = "REQUEST_UPDATE_PASSWORD_CONTENT";
export const RECEIVE_UPDATE_PASSWORD_CONTENT = "RECEIVE_UPDATE_PASSWORD_CONTENT";
export interface IRequestUpdatePasswordContent { type: typeof REQUEST_UPDATE_PASSWORD_CONTENT }
export interface IReceiveUpdatePasswordContent { type: typeof RECEIVE_UPDATE_PASSWORD_CONTENT, payload: IUpdatePasswordContentDto }
export type TKnownActions = IRequestUpdatePasswordContent | IReceiveUpdatePasswordContent;

export const ContentUpdatePasswordAction = 
{
    get: (): IApplicationAction<TKnownActions> => (dispatch, getState) =>
    {
        const content = getState().contentUpdatePassword.content;
        const languageId = getState().applicationLanguage.id;
        const isContentChanged = content !== ApplicationDefault.contentUpdatePassword.content;
        const isLanguageChanged = languageId !== content.language;

        if (isContentChanged && !isLanguageChanged) 
        {
            return;
        }

        GetContentService(
        { 
            dispatch: dispatch, 
            state: getState, 
            request: REQUEST_UPDATE_PASSWORD_CONTENT, 
            receive: RECEIVE_UPDATE_PASSWORD_CONTENT, 
            url: GET_UPDATE_PASSWORD_CONTENT 
        });
    }
}