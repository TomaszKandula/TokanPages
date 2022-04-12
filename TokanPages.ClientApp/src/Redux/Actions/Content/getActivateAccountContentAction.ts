import axios from "axios";
import { AppThunkAction } from "../../applicationState";
import { combinedDefaults } from "../../combinedDefaults";
import { RaiseError } from "../../../Shared/helpers";
import { UnexpectedStatusCode } from "../../../Shared/textWrappers";
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
    getActivateAccountContent: (isLanguageChanged: boolean = false): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
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
                    ? RaiseError(dispatch, NULL_RESPONSE_ERROR) 
                    : dispatch({ type: RECEIVE_ACTIVATE_ACCOUNT_CONTENT, payload: response.data });
            }

            RaiseError(dispatch, UnexpectedStatusCode(response.status));
        })
        .catch(error =>
        {
            RaiseError(dispatch, error);
        });
    }
}