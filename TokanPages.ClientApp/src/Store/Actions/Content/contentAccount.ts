import { IApplicationAction, ApplicationDefault } from "../../Configuration";
import { IAccountContentDto } from "../../../Api/Models";
import { GET_ACCOUNT_CONTENT } from "../../../Api/Request";
import { GetContentService } from "./Services/getContentService";

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

        GetContentService(
        { 
            dispatch: dispatch, 
            state: getState, 
            request: REQUEST, 
            receive: RECEIVE, 
            url: GET_ACCOUNT_CONTENT 
        });
    }
}