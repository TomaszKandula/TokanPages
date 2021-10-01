import axios from "axios";
import { AppThunkAction } from "../../applicationState";
import { combinedDefaults } from "../../combinedDefaults";
import { RaiseError } from "../../../Shared/helpers";
import { UnexpectedStatusCode } from "../../../Shared/textWrappers";
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
        if (getState().getUserSigninContent.content !== combinedDefaults.getUserSigninContent.content) 
            return;
        
        dispatch({ type: REQUEST_USER_SIGNIN_CONTENT });

        axios(EnrichConfiguration(
        {
            method: "GET", 
            url: GET_SIGNIN_CONTENT,
            responseType: "json"
        }))
        .then(response =>
        {
            if (response.status === 200)
            {
                return response.data === null 
                    ? RaiseError(dispatch, NULL_RESPONSE_ERROR) 
                    : dispatch({ type: RECEIVE_USER_SIGNIN_CONTENT, payload: response.data });
            }

            RaiseError(dispatch, UnexpectedStatusCode(response.status));
        })
        .catch(error =>
        {
            RaiseError(dispatch, error);
        });
    }
}