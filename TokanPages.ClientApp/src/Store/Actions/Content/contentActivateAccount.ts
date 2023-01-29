import { ApplicationAction, ApplicationDefault } from "../../Configuration";
import { GetContent, GET_ACTIVATE_ACCOUNT_CONTENT } from "../../../Api/Request";
import { ActivateAccountContentDto } from "../../../Api/Models";

export const REQUEST = "REQUEST_ACTIVATE_ACCOUNT_CONTENT";
export const RECEIVE = "RECEIVE_ACTIVATE_ACCOUNT_CONTENT";
interface Request { type: typeof REQUEST }
interface Receive { type: typeof RECEIVE, payload: ActivateAccountContentDto }
export type TKnownActions = Request | Receive;

export const ContentActivateAccountAction = 
{
    get: (): ApplicationAction<TKnownActions> => (dispatch, getState) =>
    {
        const content = getState().contentActivateAccount.content;
        const languageId = getState().applicationLanguage.id;
        const isContentChanged = content !== ApplicationDefault.contentActivateAccount.content;
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
            url: GET_ACTIVATE_ACCOUNT_CONTENT 
        });
    }
}