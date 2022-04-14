import axios from "axios";
import { AppThunkAction } from "../../applicationState";
import { combinedDefaults } from "../../combinedDefaults";
import { RaiseError } from "../../../Shared/helpers";
import { UnexpectedStatusCode } from "../../../Shared/textWrappers";
import { GET_CLIENTS_CONTENT, NULL_RESPONSE_ERROR } from "../../../Shared/constants";
import { TErrorActions } from "../raiseErrorAction";
import { IClientsContentDto } from "../../../Api/Models";
import { EnrichConfiguration } from "../../../Api/Request";
import { GetUserLanguage } from "../../../Shared/Services/languageService";

export const REQUEST_CLIENTS_CONTENT = "REQUEST_CLIENTS_CONTENT";
export const RECEIVE_CLIENTS_CONTENT = "RECEIVE_CLIENTS_CONTENT";
export interface IRequestClientsContent { type: typeof REQUEST_CLIENTS_CONTENT }
export interface IReceiveClientsContent { type: typeof RECEIVE_CLIENTS_CONTENT, payload: IClientsContentDto }
export type TKnownActions = IRequestClientsContent | IReceiveClientsContent | TErrorActions;

export const ActionCreators = 
{
    getClientsContent: (isLanguageChanged: boolean = false): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        if (getState().getArticleFeatContent.content !== combinedDefaults.getArticleFeatContent.content && !isLanguageChanged) 
            return;

        dispatch({ type: REQUEST_CLIENTS_CONTENT });

        const language = GetUserLanguage();
        const queryParam = language === "" ? "" : `&language=${language}`;

        axios(EnrichConfiguration(
        {
            method: "GET", 
            url: `${GET_CLIENTS_CONTENT}${queryParam}`,
            responseType: "json"
        }))
        .then(response =>
        {
            if (response.status === 200)
            {
                return response.data === null 
                    ? RaiseError(dispatch, NULL_RESPONSE_ERROR) 
                    : dispatch({ type: RECEIVE_CLIENTS_CONTENT, payload: response.data });
            }

            RaiseError(dispatch, UnexpectedStatusCode(response.status));
        })
        .catch(error =>
        {
            RaiseError(dispatch, error);
        });
    }
}