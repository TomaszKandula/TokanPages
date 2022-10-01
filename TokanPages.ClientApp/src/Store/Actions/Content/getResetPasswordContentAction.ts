import { AppThunkAction, ApplicationDefaults } from "../../Configuration";
import { GET_RESET_PASSWORD_CONTENT } from "../../../Shared/constants";
import { TErrorActions } from "../applicationError";
import { IResetPasswordContentDto } from "../../../Api/Models";
import { GetContent } from "./Services/getContentService";

export const REQUEST_RESET_PASSWORD_CONTENT = "REQUEST_RESET_PASSWORD_CONTENT";
export const RECEIVE_RESET_PASSWORD_CONTENT = "RECEIVE_RESET_PASSWORD_CONTENT";
export interface IRequestResetPasswordContent { type: typeof REQUEST_RESET_PASSWORD_CONTENT }
export interface IReceiveResetPasswordContent { type: typeof RECEIVE_RESET_PASSWORD_CONTENT, payload: IResetPasswordContentDto }
export type TKnownActions = IRequestResetPasswordContent | IReceiveResetPasswordContent | TErrorActions;

export const ActionCreators = 
{
    get: (): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        const isLanguageChanged = getState().applicationLanguage.id !== getState().contentResetPassword.content.language;

        if (getState().contentResetPassword.content !== ApplicationDefaults.contentResetPassword.content && !isLanguageChanged) 
        {
            return;
        }

        GetContent(
        { 
            dispatch: dispatch, 
            state: getState, 
            request: REQUEST_RESET_PASSWORD_CONTENT, 
            receive: RECEIVE_RESET_PASSWORD_CONTENT, 
            url: GET_RESET_PASSWORD_CONTENT 
        });
    }
}