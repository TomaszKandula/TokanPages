import axios from "axios";
import { AppThunkAction } from "../../applicationState";
import { combinedDefaults } from "../../combinedDefaults";
import { GetTextStatusCode } from "../../../Shared/Services/Utilities";
import { RaiseError } from "../../../Shared/Services/ErrorServices";
import { GET_SIGNUP_CONTENT, NULL_RESPONSE_ERROR } from "../../../Shared/constants";
import { TErrorActions } from "./../raiseErrorAction";
import { IUserSignupContentDto } from "../../../Api/Models";
import { EnrichConfiguration } from "../../../Api/Request";
import { GetUserLanguageFromStore } from "../../../Shared/Services/languageService";

export const REQUEST_USER_SIGNUP_CONTENT = "REQUEST_USER_SIGNUP_CONTENT";
export const RECEIVE_USER_SIGNUP_CONTENT = "RECEIVE_USER_SIGNUP_CONTENT";
export interface IRequestSignupFormContent { type: typeof REQUEST_USER_SIGNUP_CONTENT }
export interface IReceiveSignupFormContent { type: typeof RECEIVE_USER_SIGNUP_CONTENT, payload: IUserSignupContentDto }
export type TKnownActions = IRequestSignupFormContent | IReceiveSignupFormContent | TErrorActions;

export const ActionCreators = 
{
    getUserSignupContent: (): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        const isLanguageChanged = getState().userLanguage.id !== getState().getUserSignupContent.content.language;

        if (getState().getUserSignupContent.content !== combinedDefaults.getUserSignupContent.content && !isLanguageChanged) 
            return;

        dispatch({ type: REQUEST_USER_SIGNUP_CONTENT });

        const language = GetUserLanguageFromStore();
        const queryParam = language === "" ? "" : `&language=${language}`;

        axios(EnrichConfiguration(
        {
            method: "GET", 
            url: `${GET_SIGNUP_CONTENT}${queryParam}`,
            responseType: "json"
        }))
        .then(response =>
        {
            if (response.status === 200)
            {
                return response.data === null 
                    ? RaiseError({ dispatch: dispatch, errorObject: NULL_RESPONSE_ERROR }) 
                    : dispatch({ type: RECEIVE_USER_SIGNUP_CONTENT, payload: response.data });
            }

            RaiseError({ dispatch: dispatch, errorObject: GetTextStatusCode({ statusCode: response.status }) });
        })
        .catch(error =>
        {
            RaiseError({ dispatch: dispatch, errorObject: error });
        });
    }
}