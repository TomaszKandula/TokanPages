import axios from "axios";
import { AppThunkAction } from "../../applicationState";
import { combinedDefaults } from "../../../Redux/combinedDefaults";
import { GetTextStatusCode } from "../../../Shared/Services/Utilities";
import { RaiseError } from "../../../Shared/Services/ErrorServices";
import { GET_HEADER_CONTENT, NULL_RESPONSE_ERROR } from "../../../Shared/constants";
import { TErrorActions } from "./../raiseErrorAction";
import { IHeaderContentDto } from "../../../Api/Models";
import { EnrichConfiguration } from "../../../Api/Request";
import { GetUserLanguageFromStore } from "../../../Shared/Services/languageService";

export const REQUEST_HEADER_CONTENT = "REQUEST_HEADER_CONTENT";
export const RECEIVE_HEADER_CONTENT = "RECEIVE_HEADER_CONTENT";
export interface IRequestHeaderContent { type: typeof REQUEST_HEADER_CONTENT }
export interface IReceiveHeaderContent { type: typeof RECEIVE_HEADER_CONTENT, payload: IHeaderContentDto }
export type TKnownActions = IRequestHeaderContent | IReceiveHeaderContent | TErrorActions;

export const ActionCreators = 
{
    getHeaderContent: (): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        const isLanguageChanged = getState().userLanguage.id !== getState().getHeaderContent.content.language;

        if (getState().getHeaderContent.content !== combinedDefaults.getHeaderContent.content && !isLanguageChanged) 
            return;

        dispatch({ type: REQUEST_HEADER_CONTENT });

        const language = GetUserLanguageFromStore();
        const queryParam = language === "" ? "" : `&language=${language}`;

        axios(EnrichConfiguration(
        {
            method: "GET", 
            url: `${GET_HEADER_CONTENT}${queryParam}`,
            responseType: "json"
        }))
        .then(response =>
        {
            if (response.status === 200)
            {
                return response.data === null 
                    ? RaiseError({ dispatch, errorObject: NULL_RESPONSE_ERROR }) 
                    : dispatch({ type: RECEIVE_HEADER_CONTENT, payload: response.data });
            }

            RaiseError({ dispatch, errorObject: GetTextStatusCode({ statusCode: response.status }) });
        })
        .catch(error =>
        {
            RaiseError({ dispatch: dispatch, errorObject: error });
        });
    }
}