import axios from "axios";
import { AppThunkAction } from "../applicationState";
import { combinedDefaults } from "../../Redux/combinedDefaults";
import { RaiseError } from "../../Shared/helpers";
import { UnexpectedStatusCode } from "../../Shared/textWrappers";
import { GET_SIGNUP_FORM_CONTENT, NULL_RESPONSE_ERROR } from "../../Shared/constants";
import { TErrorActions } from "./raiseErrorAction";
import { ISignupFormContentDto } from "../../Api/Models";

export const REQUEST_SIGNUP_FORM_CONTENT = "REQUEST_SIGNUP_FORM_CONTENT";
export const RECEIVE_SIGNUP_FORM_CONTENT = "RECEIVE_SIGNUP_FORM_CONTENT";
export interface IRequestSignupFormContent { type: typeof REQUEST_SIGNUP_FORM_CONTENT }
export interface IReceiveSignupFormContent { type: typeof RECEIVE_SIGNUP_FORM_CONTENT, payload: ISignupFormContentDto }
export type TKnownActions = IRequestSignupFormContent | IReceiveSignupFormContent | TErrorActions;

export const ActionCreators = 
{
    getSignupFormContent: (): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        if (getState().getSignupFormContent.content !== combinedDefaults.getSignupFormContent.content) 
            return;

        dispatch({ type: REQUEST_SIGNUP_FORM_CONTENT });

        axios( 
        {
            method: "GET", 
            url: GET_SIGNUP_FORM_CONTENT,
            responseType: "json"
        })
        .then(response =>
        {
            if (response.status === 200)
            {
                return response.data === null 
                    ? RaiseError(dispatch, NULL_RESPONSE_ERROR) 
                    : dispatch({ type: RECEIVE_SIGNUP_FORM_CONTENT, payload: response.data });
            }

            RaiseError(dispatch, UnexpectedStatusCode(response.status));
        })
        .catch(error =>
        {
            RaiseError(dispatch, error);
        });
    }
}