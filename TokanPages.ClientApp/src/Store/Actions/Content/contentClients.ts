import { IApplicationAction, ApplicationDefault } from "../../Configuration";
import { GET_CLIENTS_CONTENT } from "../../../Api/Request";
import { IClientsContentDto } from "../../../Api/Models";
import { GetContentService } from "./Services/getContentService";

export const REQUEST = "REQUEST_CLIENTS_CONTENT";
export const RECEIVE = "RECEIVE_CLIENTS_CONTENT";
interface IRequest { type: typeof REQUEST }
interface IReceive { type: typeof RECEIVE, payload: IClientsContentDto }
export type TKnownActions = IRequest | IReceive;

export const ContentClientsAction = 
{
    get: (): IApplicationAction<TKnownActions> => (dispatch, getState) =>
    {
        const content = getState().contentClients.content;
        const languageId = getState().applicationLanguage.id;
        const isContentChanged = content !== ApplicationDefault.contentClients.content;
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
            url: GET_CLIENTS_CONTENT 
        });
    }
}