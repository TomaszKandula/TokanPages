import axios from "axios";
import { AppThunkAction } from "../../applicationState";
import { combinedDefaults } from "../../combinedDefaults";
import { GetTextStatusCode } from "../../../Shared/Services/Utilities";
import { RaiseError } from "../../../Shared/Services/ErrorServices";
import { GET_ACTIVATE_ACCOUNT_CONTENT, NULL_RESPONSE_ERROR } from "../../../Shared/constants";
import { TErrorActions } from "../raiseErrorAction";
import { IActivateAccountContentDto } from "../../../Api/Models";
import { EnrichConfiguration } from "../../../Api/Request";
import { GetUserLanguage } from "../../../Shared/Services/languageService";

export const REQUEST_ACTIVATE_ACCOUNT_CONTENT = "REQUEST_ACTIVATE_ACCOUNT_CONTENT";
export const RECEIVE_ACTIVATE_ACCOUNT_CONTENT = "RECEIVE_ACTIVATE_ACCOUNT_CONTENT";
export interface IRequestActivateAccountContent { type: typeof REQUEST_ACTIVATE_ACCOUNT_CONTENT }
export interface IReceiveActivateAccountContent { type: typeof RECEIVE_ACTIVATE_ACCOUNT_CONTENT, payload: IActivateAccountContentDto }
export type TKnownActions = IRequestActivateAccountContent | IReceiveActivateAccountContent | TErrorActions;

export const ActionCreators = 
{
    getActivateAccountContent: (): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        const isLanguageChanged = getState().userLanguage.id !== getState().getActivateAccountContent.content.language;

        if (getState().getActivateAccountContent.content !== combinedDefaults.getActivateAccountContent.content && !isLanguageChanged) 
            return;

        dispatch({ type: REQUEST_ACTIVATE_ACCOUNT_CONTENT });

        const language = GetUserLanguage();
        const queryParam = language === "" ? "" : `&language=${language}`;

        axios(EnrichConfiguration(
        {
            method: "GET", 
            url: `${GET_ACTIVATE_ACCOUNT_CONTENT}${queryParam}`,
            responseType: "json"
        }))
        .then(response =>
        {
            if (response.status === 200)
            {
                return response.data === null 
                    ? RaiseError({ dispatch, errorObject: NULL_RESPONSE_ERROR }) 
                    : dispatch({ type: RECEIVE_ACTIVATE_ACCOUNT_CONTENT, payload: response.data });
            }

            RaiseError({ dispatch, errorObject: GetTextStatusCode({ statusCode: response.status }) });
        })
        .catch(error =>
        {
            RaiseError({ dispatch: dispatch, errorObject: error });
        });
    }
}