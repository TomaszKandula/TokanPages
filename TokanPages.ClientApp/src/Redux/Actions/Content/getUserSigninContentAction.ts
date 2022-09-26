import axios from "axios";
import { AppThunkAction } from "../../applicationState";
import { combinedDefaults } from "../../combinedDefaults";
import { GetTextStatusCode } from "../../../Shared/Services/Utilities";
import { RaiseError } from "../../../Shared/Services/ErrorServices";
import { GET_SIGNIN_CONTENT, NULL_RESPONSE_ERROR } from "../../../Shared/constants";
import { TErrorActions } from "./../raiseErrorAction";
import { IUserSigninContentDto } from "../../../Api/Models";
import { EnrichConfiguration } from "../../../Api/Request";

export const REQUEST_USER_SIGNIN_CONTENT = "REQUEST_USER_SIGNIN_CONTENT";
export const RECEIVE_USER_SIGNIN_CONTENT = "RECEIVE_USER_SIGNIN_CONTENT";
export interface IRequestSigninFormContent { type: typeof REQUEST_USER_SIGNIN_CONTENT }
export interface IReceiveSigninFormContent { type: typeof RECEIVE_USER_SIGNIN_CONTENT, payload: IUserSigninContentDto }
export type TKnownActions = IRequestSigninFormContent | IReceiveSigninFormContent | TErrorActions;

export const ActionCreators = 
{
    getUserSigninContent: (): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        const isLanguageChanged = getState().userLanguage.id !== getState().getUserSigninContent.content.language;

        if (getState().getUserSigninContent.content !== combinedDefaults.getUserSigninContent.content && !isLanguageChanged) 
            return;
        
        dispatch({ type: REQUEST_USER_SIGNIN_CONTENT });

        const id = getState().userLanguage.id;
        const queryParam = id === "" ? "" : `&language=${id}`;

        axios(EnrichConfiguration(
        {
            method: "GET", 
            url: `${GET_SIGNIN_CONTENT}${queryParam}`,
            responseType: "json"
        }))
        .then(response =>
        {
            if (response.status === 200)
            {
                return response.data === null 
                    ? RaiseError({ dispatch: dispatch, errorObject: NULL_RESPONSE_ERROR }) 
                    : dispatch({ type: RECEIVE_USER_SIGNIN_CONTENT, payload: response.data });
            }

            RaiseError({ dispatch: dispatch, errorObject: GetTextStatusCode({ statusCode: response.status }) });
        })
        .catch(error =>
        {
            RaiseError({ dispatch: dispatch, errorObject: error });
        });
    }
}