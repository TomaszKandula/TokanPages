import { AppThunkAction, CombinedDefaults } from "../../Configuration";
import { GET_CLIENTS_CONTENT } from "../../../Shared/constants";
import { TErrorActions } from "../raiseErrorAction";
import { IClientsContentDto } from "../../../Api/Models";
import { GetContent } from "./Services/getContentService";

export const REQUEST_CLIENTS_CONTENT = "REQUEST_CLIENTS_CONTENT";
export const RECEIVE_CLIENTS_CONTENT = "RECEIVE_CLIENTS_CONTENT";
export interface IRequestClientsContent { type: typeof REQUEST_CLIENTS_CONTENT }
export interface IReceiveClientsContent { type: typeof RECEIVE_CLIENTS_CONTENT, payload: IClientsContentDto }
export type TKnownActions = IRequestClientsContent | IReceiveClientsContent | TErrorActions;

export const ActionCreators = 
{
    getClientsContent: (): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        const isLanguageChanged = getState().userLanguage.id !== getState().getClientsContent.content.language;

        if (getState().getClientsContent.content !== CombinedDefaults.getClientsContent.content && !isLanguageChanged) 
        {
            return;
        }

        GetContent(
        { 
            dispatch: dispatch, 
            state: getState, 
            request: REQUEST_CLIENTS_CONTENT, 
            receive: RECEIVE_CLIENTS_CONTENT, 
            url: GET_CLIENTS_CONTENT 
        });
    }
}