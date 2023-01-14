import { IApplicationAction, ApplicationDefault } from "../../Configuration";
import { IAccountContentDto } from "../../../Api/Models";
import { GetContent, GET_ACCOUNT_CONTENT } from "../../../Api/Request";

export const REQUEST = "REQUEST_ACCOUNT_CONTENT";
export const RECEIVE = "RECEIVE_ACCOUNT_CONTENT";
interface IRequest { type: typeof REQUEST }
interface IReceive { type: typeof RECEIVE, payload: IAccountContentDto }
export type TKnownActions = IRequest | IReceive;

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

        GetContent(
        { 
            dispatch: dispatch, 
            state: getState, 
            request: REQUEST, 
            receive: RECEIVE, 
            url: GET_ACCOUNT_CONTENT 
        });
    }
}