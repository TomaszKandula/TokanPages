import { ApplicationAction, ApplicationDefault } from "../../Configuration";
import { GetContent, GET_RESET_PASSWORD_CONTENT } from "../../../Api/Request";
import { ResetPasswordContentDto } from "../../../Api/Models";

export const REQUEST = "REQUEST_RESET_PASSWORD_CONTENT";
export const RECEIVE = "RECEIVE_RESET_PASSWORD_CONTENT";
interface Request { type: typeof REQUEST }
interface Receive { type: typeof RECEIVE, payload: ResetPasswordContentDto }
export type TKnownActions = Request | Receive;

export const ContentResetPasswordAction = 
{
    get: (): ApplicationAction<TKnownActions> => (dispatch, getState) =>
    {
        const content = getState().contentResetPassword.content;
        const languageId = getState().applicationLanguage.id;
        const isContentChanged = content !== ApplicationDefault.contentResetPassword.content;
        const isLanguageChanged = languageId !== content.language;

        if (isContentChanged && !isLanguageChanged) 
        {
            return;
        }

        GetContent(
        { 
            dispatch: dispatch, 
            state: getState, 
            request: REQUEST, 
            receive: RECEIVE, 
            url: GET_RESET_PASSWORD_CONTENT 
        });
    }
}