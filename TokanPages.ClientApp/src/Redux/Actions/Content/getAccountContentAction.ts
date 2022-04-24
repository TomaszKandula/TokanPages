import axios from "axios";
import { AppThunkAction } from "../../applicationState";
import { combinedDefaults } from "../../combinedDefaults";
import { GetTextStatusCode } from "../../../Shared/Services/Utilities";
import { RaiseError } from "../../../Shared/Services/ErrorServices";
import { GET_ACCOUNT_CONTENT, NULL_RESPONSE_ERROR } from "../../../Shared/constants";
import { TErrorActions } from "../raiseErrorAction";
import { IAccountContentDto } from "../../../Api/Models";
import { EnrichConfiguration } from "../../../Api/Request";
import { GetUserLanguage } from "../../../Shared/Services/languageService";

export const REQUEST_ACCOUNT_CONTENT = "REQUEST_ACCOUNT_CONTENT";
export const RECEIVE_ACCOUNT_CONTENT = "RECEIVE_ACCOUNT_CONTENT";
export interface IRequestAccountContent { type: typeof REQUEST_ACCOUNT_CONTENT }
export interface IReceiveAccountContent { type: typeof RECEIVE_ACCOUNT_CONTENT, payload: IAccountContentDto }
export type TKnownActions = IRequestAccountContent | IReceiveAccountContent | TErrorActions;

export const ActionCreators = 
{
    getAccountContent: (isLanguageChanged: boolean = false): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        if (getState().getAccountContent.content !== combinedDefaults.getAccountContent.content && !isLanguageChanged) 
            return;

        dispatch({ type: REQUEST_ACCOUNT_CONTENT });

        const language = GetUserLanguage();
        const queryParam = language === "" ? "" : `&language=${language}`;

        axios(EnrichConfiguration(
        {
            method: "GET", 
            url: `${GET_ACCOUNT_CONTENT}${queryParam}`,
            responseType: "json"
        }))
        .then(response =>
        {
            if (response.status === 200)
            {
                return response.data === null 
                    ? RaiseError({ dispatch, errorObject: NULL_RESPONSE_ERROR }) 
                    : dispatch({ type: RECEIVE_ACCOUNT_CONTENT, payload: response.data });
            }

            RaiseError({ dispatch, errorObject: GetTextStatusCode({ statusCode: response.status }) });
        })
        .catch(error =>
        {
            RaiseError({ dispatch: dispatch, errorObject: error });
        });
    }
}