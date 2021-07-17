import axios from "axios";
import * as Sentry from "@sentry/react";
import { AppThunkAction } from "../applicationState";
import { combinedDefaults } from "../../Redux/combinedDefaults";
import { GetErrorMessage } from "../../Shared/helpers";
import { UnexpectedStatusCode } from "../../Shared/textWrappers";
import { GET_WRONG_PAGE_PROMPT_CONTENT } from "../../Shared/constants";
import { RAISE_ERROR, TErrorActions } from "./raiseErrorAction";
import { IWrongPagePromptContentDto } from "../../Api/Models";

export const REQUEST_WRONG_PAGE_CONTENT = "REQUEST_WRONG_PAGE_CONTENT";
export const RECEIVE_WRONG_PAGE_CONTENT = "RECEIVE_WRONG_PAGE_CONTENT";

export interface IRequestWrongPageContent { type: typeof REQUEST_WRONG_PAGE_CONTENT }
export interface IReceiveWrongPageContent { type: typeof RECEIVE_WRONG_PAGE_CONTENT, payload: IWrongPagePromptContentDto }

export type TKnownActions = IRequestWrongPageContent | IReceiveWrongPageContent | TErrorActions;

export const ActionCreators = 
{
    getWrongPagePromptContent: (): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        if (getState().getWrongPagePromptContent.content !== combinedDefaults.getWrongPagePromptContent.content)
            return;

        dispatch({ type: REQUEST_WRONG_PAGE_CONTENT });

        axios( 
        {
            method: "GET", 
            url: GET_WRONG_PAGE_PROMPT_CONTENT,
            responseType: "json"
        })
        .then(response =>
        {
            if (response.status === 200)
            {
                dispatch({ type: RECEIVE_WRONG_PAGE_CONTENT, payload: response.data });
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