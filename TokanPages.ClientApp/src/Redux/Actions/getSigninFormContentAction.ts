import axios from "axios";
import { AppThunkAction } from "../applicationState";
import { combinedDefaults } from "../../Redux/combinedDefaults";
import { RaiseError } from "../../Shared/helpers";
import { UnexpectedStatusCode } from "../../Shared/textWrappers";
import { GET_SIGNIN_FORM_CONTENT, NULL_RESPONSE_ERROR } from "../../Shared/constants";
import { TErrorActions } from "./raiseErrorAction";
import { ISigninFormContentDto } from "../../Api/Models";

export const REQUEST_SIGNIN_FORM_CONTENT = "REQUEST_SIGNIN_FORM_CONTENT";
export const RECEIVE_SIGNIN_FORM_CONTENT = "RECEIVE_SIGNIN_FORM_CONTENT";
export interface IRequestSigninFormContent { type: typeof REQUEST_SIGNIN_FORM_CONTENT }
export interface IReceiveSigninFormContent { type: typeof RECEIVE_SIGNIN_FORM_CONTENT, payload: ISigninFormContentDto }
export type TKnownActions = IRequestSigninFormContent | IReceiveSigninFormContent | TErrorActions;

export const ActionCreators = 
{
    getSigninFormContent: (): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        if (getState().getSigninFormContent.content !== combinedDefaults.getSigninFormContent.content) 
            return;
        
        dispatch({ type: REQUEST_SIGNIN_FORM_CONTENT });

        axios( 
        {
            method: "GET", 
            url: GET_SIGNIN_FORM_CONTENT,
            responseType: "json"
        })
        .then(response =>
        {
            if (response.status === 200)
            {
                return response.data === null 
                    ? RaiseError(dispatch, NULL_RESPONSE_ERROR) 
                    : dispatch({ type: RECEIVE_SIGNIN_FORM_CONTENT, payload: response.data });
            }

            RaiseError(dispatch, UnexpectedStatusCode(response.status));
        })
        .catch(error =>
        {
            RaiseError(dispatch, error);
        });
    }
}