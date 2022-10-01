import { IApplicationAction, ApplicationDefault } from "../../Configuration";
import { GET_CLIENTS_CONTENT } from "../../../Shared/constants";
import { IClientsContentDto } from "../../../Api/Models";
import { GetContentService } from "./Services/getContentService";

export const REQUEST_CLIENTS_CONTENT = "REQUEST_CLIENTS_CONTENT";
export const RECEIVE_CLIENTS_CONTENT = "RECEIVE_CLIENTS_CONTENT";
export interface IRequestClientsContent { type: typeof REQUEST_CLIENTS_CONTENT }
export interface IReceiveClientsContent { type: typeof RECEIVE_CLIENTS_CONTENT, payload: IClientsContentDto }
export type TKnownActions = IRequestClientsContent | IReceiveClientsContent;

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
            request: REQUEST_CLIENTS_CONTENT, 
            receive: RECEIVE_CLIENTS_CONTENT, 
            url: GET_CLIENTS_CONTENT 
        });
    }
}