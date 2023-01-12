import { IApplicationAction, ApplicationDefault } from "../../Configuration";
import { GetContent, GET_UPDATE_PASSWORD_CONTENT } from "../../../Api/Request";
import { IUpdatePasswordContentDto } from "../../../Api/Models";

export const REQUEST = "REQUEST_UPDATE_PASSWORD_CONTENT";
export const RECEIVE = "RECEIVE_UPDATE_PASSWORD_CONTENT";
interface IRequest { type: typeof REQUEST }
interface IReceive { type: typeof RECEIVE, payload: IUpdatePasswordContentDto }
export type TKnownActions = IRequest | IReceive;

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

        GetContent(
        { 
            dispatch: dispatch, 
            state: getState, 
            request: REQUEST, 
            receive: RECEIVE, 
            url: GET_UPDATE_PASSWORD_CONTENT 
        });
    }
}