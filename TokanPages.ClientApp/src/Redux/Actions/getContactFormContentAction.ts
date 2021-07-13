import axios from "axios";
import * as Sentry from "@sentry/react";
import { AppThunkAction } from "../applicationState";
import { combinedDefaults } from "../../Redux/combinedDefaults";
import { GetErrorMessage } from "../../Shared/helpers";
import { UnexpectedStatusCode } from "../../Shared/textWrappers";
import { GET_CONTACT_FORM_CONTENT } from "../../Shared/constants";
import { RAISE_ERROR, TErrorActions } from "./raiseErrorAction";
import { IContactFormContentDto } from "../../Api/Models";

export const REQUEST_CONTACT_FORM_CONTENT = "REQUEST_CONTACT_FORM_CONTENT";
export const RECEIVE_CONTACT_FORM_CONTENT = "RECEIVE_CONTACT_FORM_CONTENT";

export interface IRequestContactFormContent { type: typeof REQUEST_CONTACT_FORM_CONTENT }
export interface IReceiveContactFormContent { type: typeof RECEIVE_CONTACT_FORM_CONTENT, payload: IContactFormContentDto }

export type TKnownActions = IRequestContactFormContent | IReceiveContactFormContent | TErrorActions;

export const ActionCreators = 
{
    getContactFormContent: (): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        if (getState().getContactFormContent.content !== combinedDefaults.getContactFormContent.content) 
            return;

        dispatch({ type: REQUEST_CONTACT_FORM_CONTENT });

        axios( 
        {
            method: "GET", 
            url: GET_CONTACT_FORM_CONTENT,
            responseType: "json"
        })
        .then(response =>
        {
            if (response.status === 200)
            {
                dispatch({ type: RECEIVE_CONTACT_FORM_CONTENT, payload: response.data });
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