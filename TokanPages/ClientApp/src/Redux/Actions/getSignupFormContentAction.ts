import axios from "axios";
import * as Sentry from "@sentry/react";
import { AppThunkAction } from "../applicationState";
import { combinedDefaults } from "../../Redux/combinedDefaults";
import { GetErrorMessage } from "../../Shared/helpers";
import { UnexpectedStatusCode } from "../../Shared/textWrappers";
import { GET_SIGNUP_FORM_CONTENT } from "../../Shared/constants";
import { RAISE_ERROR, TErrorActions } from "./raiseErrorAction";
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

        axios.get(GET_SIGNUP_FORM_CONTENT, 
        {
            method: "GET", 
            responseType: "json"
        })
        .then(response =>
        {
            if (response.status === 200)
            {
                dispatch({ type: RECEIVE_SIGNUP_FORM_CONTENT, payload: response.data });
                return;
            }
            
            const error = UnexpectedStatusCode(response.status);
            dispatch({ type: RAISE_ERROR, errorObject: error });
            Sentry.captureException(error);
        })
        .catch(error =>
        {
            dispatch({ type: RAISE_ERROR, errorObject: GetErrorMessage(error) });
            Sentry.captureException(error);
        });
    }
}