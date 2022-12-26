import { IApplicationAction, ApplicationDefault } from "../../Configuration";
import { GET_ACTIVATE_ACCOUNT_CONTENT } from "../../../Api/Request";
import { IActivateAccountContentDto } from "../../../Api/Models";
import { GetContentService } from "./Services/getContentService";

export const REQUEST = "REQUEST_ACTIVATE_ACCOUNT_CONTENT";
export const RECEIVE = "RECEIVE_ACTIVATE_ACCOUNT_CONTENT";
interface IRequest { type: typeof REQUEST }
interface IReceive { type: typeof RECEIVE, payload: IActivateAccountContentDto }
export type TKnownActions = IRequest | IReceive;

export const ContentActivateAccountAction = 
{
    get: (): IApplicationAction<TKnownActions> => (dispatch, getState) =>
    {
        const content = getState().contentActivateAccount.content;
        const languageId = getState().applicationLanguage.id;
        const isContentChanged = content !== ApplicationDefault.contentActivateAccount.content;
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
            url: GET_ACTIVATE_ACCOUNT_CONTENT 
        });
    }
}