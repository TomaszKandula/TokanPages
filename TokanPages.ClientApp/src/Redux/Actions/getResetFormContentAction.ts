import axios from "axios";
import * as Sentry from "@sentry/react";
import { AppThunkAction } from "../applicationState";
import { combinedDefaults } from "../../Redux/combinedDefaults";
import { GetErrorMessage } from "../../Shared/helpers";
import { UnexpectedStatusCode } from "../../Shared/textWrappers";
import { GET_RESET_FORM_CONTENT } from "../../Shared/constants";
import { RAISE_ERROR, TErrorActions } from "./raiseErrorAction";
import { IResetFormContentDto } from "../../Api/Models";

export const REQUEST_RESET_FORM_CONTENT = "REQUEST_RESET_FORM_CONTENT";
export const RECEIVE_RESET_FORM_CONTENT = "RECEIVE_RESET_FORM_CONTENT";

export interface IRequestResetFormContent { type: typeof REQUEST_RESET_FORM_CONTENT }
export interface IReceiveResetFormContent { type: typeof RECEIVE_RESET_FORM_CONTENT, payload: IResetFormContentDto }

export type TKnownActions = IRequestResetFormContent | IReceiveResetFormContent | TErrorActions;

export const ActionCreators = 
{
    getResetFormContent: (): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        if (getState().getResetFormContent.content !== combinedDefaults.getResetFormContent.content) 
            return;

        dispatch({ type: REQUEST_RESET_FORM_CONTENT });

        axios( 
        {
            method: "GET", 
            url: GET_RESET_FORM_CONTENT,
            responseType: "json"
        })
        .then(response =>
        {
            if (response.status === 200)
            {
                dispatch({ type: RECEIVE_RESET_FORM_CONTENT, payload: response.data });
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