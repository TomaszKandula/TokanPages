import axios from "axios";
import { AppThunkAction } from "../../applicationState";
import { combinedDefaults } from "../../combinedDefaults";
import { GetTextStatusCode } from "../../../Shared/Services/Utilities";
import { RaiseError } from "../../../Shared/Services/ErrorServices";
import { GET_UPDATE_PASSWORD_CONTENT, NULL_RESPONSE_ERROR } from "../../../Shared/constants";
import { TErrorActions } from "../raiseErrorAction";
import { IUpdatePasswordContentDto } from "../../../Api/Models";
import { EnrichConfiguration } from "../../../Api/Request";
import { GetUserLanguage } from "../../../Shared/Services/languageService";

export const REQUEST_UPDATE_PASSWORD_CONTENT = "REQUEST_UPDATE_PASSWORD_CONTENT";
export const RECEIVE_UPDATE_PASSWORD_CONTENT = "RECEIVE_UPDATE_PASSWORD_CONTENT";
export interface IRequestUpdatePasswordContent { type: typeof REQUEST_UPDATE_PASSWORD_CONTENT }
export interface IReceiveUpdatePasswordContent { type: typeof RECEIVE_UPDATE_PASSWORD_CONTENT, payload: IUpdatePasswordContentDto }
export type TKnownActions = IRequestUpdatePasswordContent | IReceiveUpdatePasswordContent | TErrorActions;

export const ActionCreators = 
{
    getUpdatePasswordContent: (isLanguageChanged: boolean = false): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        if (getState().getUpdatePasswordContent.content !== combinedDefaults.getUpdatePasswordContent.content && !isLanguageChanged) 
            return;

        dispatch({ type: REQUEST_UPDATE_PASSWORD_CONTENT });

        const language = GetUserLanguage();
        const queryParam = language === "" ? "" : `&language=${language}`;

        axios(EnrichConfiguration(
        {
            method: "GET", 
            url: `${GET_UPDATE_PASSWORD_CONTENT}${queryParam}`,
            responseType: "json"
        }))
        .then(response =>
        {
            if (response.status === 200)
            {
                return response.data === null 
                    ? RaiseError({ dispatch, errorObject: NULL_RESPONSE_ERROR }) 
                    : dispatch({ type: RECEIVE_UPDATE_PASSWORD_CONTENT, payload: response.data });
            }

            RaiseError({ dispatch, errorObject: GetTextStatusCode({ statusCode: response.status }) });
        })
        .catch(error =>
        {
            RaiseError({ dispatch: dispatch, errorObject: error });
        });
    }
}