import axios from "axios";
import * as Sentry from "@sentry/react";
import { AppThunkAction } from "../applicationState";
import { GetErrorMessage } from "../../Shared/helpers";
import { UnexpectedStatusCode } from "../../Shared/textWrappers";
import { GET_SIGNIN_FORM_CONTENT } from "../../Shared/constants";
import { RAISE_ERROR, TErrorActions } from "./raiseErrorAction";
import { ISigninFormContentDto } from "../../Api/Models";

export const REQUEST_SIGNIN_FORM_CONTENT = "REQUEST_SIGNIN_FORM_CONTENT";
export const RECEIVE_SIGNIN_FORM_CONTENT = "RECEIVE_SIGNIN_FORM_CONTENT";

export interface IRequestSigninFormContent { type: typeof REQUEST_SIGNIN_FORM_CONTENT }
export interface IReceiveSigninFormContent { type: typeof RECEIVE_SIGNIN_FORM_CONTENT, payload: ISigninFormContentDto }

export type TKnownActions = IRequestSigninFormContent | IReceiveSigninFormContent | TErrorActions;

export const ActionCreators = 
{
    getSigninFormContent: (): AppThunkAction<TKnownActions> => (dispatch) =>
    {
        dispatch({ type: REQUEST_SIGNIN_FORM_CONTENT });

        axios.get(GET_SIGNIN_FORM_CONTENT, 
        {
            method: "GET", 
            responseType: "json"
        })
        .then(response =>
        {
            if (response.status === 200)
            {
                dispatch({ type: RECEIVE_SIGNIN_FORM_CONTENT, payload: response.data });
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