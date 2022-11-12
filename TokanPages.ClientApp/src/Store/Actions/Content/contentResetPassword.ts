import { IApplicationAction, ApplicationDefault } from "../../Configuration";
import { GET_RESET_PASSWORD_CONTENT } from "../../../Api/Request";
import { IResetPasswordContentDto } from "../../../Api/Models";
import { GetContentService } from "./Services/getContentService";

export const REQUEST = "REQUEST_RESET_PASSWORD_CONTENT";
export const RECEIVE = "RECEIVE_RESET_PASSWORD_CONTENT";
interface IRequest { type: typeof REQUEST }
interface IReceive { type: typeof RECEIVE, payload: IResetPasswordContentDto }
export type TKnownActions = IRequest | IReceive;

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
            request: REQUEST, 
            receive: RECEIVE, 
            url: GET_RESET_PASSWORD_CONTENT 
        });
    }
}