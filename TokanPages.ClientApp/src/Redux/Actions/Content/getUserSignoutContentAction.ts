import axios from "axios";
import { AppThunkAction } from "../../applicationState";
import { combinedDefaults } from "../../combinedDefaults";
import { RaiseError } from "../../../Shared/helpers";
import { UnexpectedStatusCode } from "../../../Shared/textWrappers";
import { GET_SIGNOUT_CONTENT, NULL_RESPONSE_ERROR } from "../../../Shared/constants";
import { TErrorActions } from "./../raiseErrorAction";
import { IUserSignoutContentDto } from "../../../Api/Models";
import { EnrichConfiguration } from "../../../Api/Request";
import { GetUserLanguage } from "../../../Shared/Services/languageService";

export const REQUEST_USER_SIGNOUT_CONTENT = "REQUEST_USER_SIGNOUT_CONTENT";
export const RECEIVE_USER_SIGNOUT_CONTENT = "RECEIVE_USER_SIGNOUT_CONTENT";
export interface IRequestSignoutFormContent { type: typeof REQUEST_USER_SIGNOUT_CONTENT }
export interface IReceiveSignoutFormContent { type: typeof RECEIVE_USER_SIGNOUT_CONTENT, payload: IUserSignoutContentDto }
export type TKnownActions = IRequestSignoutFormContent | IReceiveSignoutFormContent | TErrorActions;

export const ActionCreators = 
{
    getUserSignoutContent: (isLanguageChanged: boolean = false): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        if (getState().getUserSignoutContent.content !== combinedDefaults.getUserSignoutContent.content && !isLanguageChanged) 
            return;

        dispatch({ type: REQUEST_USER_SIGNOUT_CONTENT });

        const language = GetUserLanguage();
        const queryParam = language === "" ? "" : `&language=${language}`;

        axios(EnrichConfiguration(
        {
            method: "GET", 
            url: `${GET_SIGNOUT_CONTENT}${queryParam}`,
            responseType: "json"
        }))
        .then(response =>
        {
            if (response.status === 200)
            {
                return response.data === null 
                    ? RaiseError(dispatch, NULL_RESPONSE_ERROR) 
                    : dispatch({ type: RECEIVE_USER_SIGNOUT_CONTENT, payload: response.data });
            }

            RaiseError(dispatch, UnexpectedStatusCode(response.status));
        })
        .catch(error =>
        {
            RaiseError(dispatch, error);
        });
    }
}