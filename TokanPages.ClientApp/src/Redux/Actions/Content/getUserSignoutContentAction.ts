import axios from "axios";
import { AppThunkAction } from "../../applicationState";
import { combinedDefaults } from "../../combinedDefaults";
import { GetTextStatusCode } from "../../../Shared/Services/Utilities";
import { RaiseError } from "../../../Shared/Services/ErrorServices";
import { GET_SIGNOUT_CONTENT, NULL_RESPONSE_ERROR } from "../../../Shared/constants";
import { TErrorActions } from "./../raiseErrorAction";
import { IUserSignoutContentDto } from "../../../Api/Models";
import { EnrichConfiguration } from "../../../Api/Request";

export const REQUEST_USER_SIGNOUT_CONTENT = "REQUEST_USER_SIGNOUT_CONTENT";
export const RECEIVE_USER_SIGNOUT_CONTENT = "RECEIVE_USER_SIGNOUT_CONTENT";
export interface IRequestSignoutFormContent { type: typeof REQUEST_USER_SIGNOUT_CONTENT }
export interface IReceiveSignoutFormContent { type: typeof RECEIVE_USER_SIGNOUT_CONTENT, payload: IUserSignoutContentDto }
export type TKnownActions = IRequestSignoutFormContent | IReceiveSignoutFormContent | TErrorActions;

export const ActionCreators = 
{
    getUserSignoutContent: (): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        const isLanguageChanged = getState().userLanguage.id !== getState().getUserSignoutContent.content.language;

        if (getState().getUserSignoutContent.content !== combinedDefaults.getUserSignoutContent.content && !isLanguageChanged) 
            return;

        dispatch({ type: REQUEST_USER_SIGNOUT_CONTENT });

        const id = getState().userLanguage.id;
        const queryParam = id === "" ? "" : `&language=${id}`;

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
                    ? RaiseError({ dispatch: dispatch, errorObject: NULL_RESPONSE_ERROR }) 
                    : dispatch({ type: RECEIVE_USER_SIGNOUT_CONTENT, payload: response.data });
            }

            RaiseError({ dispatch: dispatch, errorObject: GetTextStatusCode({ statusCode: response.status }) });
        })
        .catch(error =>
        {
            RaiseError({ dispatch: dispatch, errorObject: error });
        });
    }
}