import axios from "axios";
import { AppThunkAction } from "../../applicationState";
import { combinedDefaults } from "../../combinedDefaults";
import { GetTextStatusCode } from "../../../Shared/Services/Utilities";
import { RaiseError } from "../../../Shared/Services/ErrorServices";
import { GET_CLIENTS_CONTENT, NULL_RESPONSE_ERROR } from "../../../Shared/constants";
import { TErrorActions } from "../raiseErrorAction";
import { IClientsContentDto } from "../../../Api/Models";
import { EnrichConfiguration } from "../../../Api/Request";

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

        if (getState().getClientsContent.content !== combinedDefaults.getClientsContent.content && !isLanguageChanged) 
            return;

        dispatch({ type: REQUEST_CLIENTS_CONTENT });

        const id = getState().userLanguage.id;
        const queryParam = id === "" ? "" : `&language=${id}`;

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
                    ? RaiseError({ dispatch, errorObject: NULL_RESPONSE_ERROR }) 
                    : dispatch({ type: RECEIVE_CLIENTS_CONTENT, payload: response.data });
            }

            RaiseError({ dispatch, errorObject: GetTextStatusCode({ statusCode: response.status }) });
        })
        .catch(error =>
        {
            RaiseError({ dispatch: dispatch, errorObject: error });
        });
    }
}