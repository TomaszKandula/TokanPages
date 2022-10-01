import { IApplicationAction, ApplicationDefault } from "../../Configuration";
import { GET_RESET_PASSWORD_CONTENT } from "../../../Shared/constants";
import { IResetPasswordContentDto } from "../../../Api/Models";
import { GetContentService } from "./Services/getContentService";

export const REQUEST_RESET_PASSWORD_CONTENT = "REQUEST_RESET_PASSWORD_CONTENT";
export const RECEIVE_RESET_PASSWORD_CONTENT = "RECEIVE_RESET_PASSWORD_CONTENT";
export interface IRequestResetPasswordContent { type: typeof REQUEST_RESET_PASSWORD_CONTENT }
export interface IReceiveResetPasswordContent { type: typeof RECEIVE_RESET_PASSWORD_CONTENT, payload: IResetPasswordContentDto }
export type TKnownActions = IRequestResetPasswordContent | IReceiveResetPasswordContent;

export const ContentResetPasswordAction = 
{
    get: (): IApplicationAction<TKnownActions> => (dispatch, getState) =>
    {
        const content = getState().contentResetPassword.content;
        const languageId = getState().applicationLanguage.id;
        const isContentChanged = content !== ApplicationDefault.contentResetPassword.content;
        const isLanguageChanged = languageId !== content.language;

        if (isContentChanged && !isLanguageChanged) 
        {
            return;
        }

        GetContentService(
        { 
            dispatch: dispatch, 
            state: getState, 
            request: REQUEST_RESET_PASSWORD_CONTENT, 
            receive: RECEIVE_RESET_PASSWORD_CONTENT, 
            url: GET_RESET_PASSWORD_CONTENT 
        });
    }
}