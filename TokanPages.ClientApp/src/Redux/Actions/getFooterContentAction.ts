import axios from "axios";
import * as Sentry from "@sentry/react";
import { AppThunkAction } from "../applicationState";
import { combinedDefaults } from "../../Redux/combinedDefaults";
import { GetErrorMessage } from "../../Shared/helpers";
import { UnexpectedStatusCode } from "../../Shared/textWrappers";
import { GET_FOOTER_CONTENT } from "../../Shared/constants";
import { RAISE_ERROR, TErrorActions } from "./raiseErrorAction";
import { IFooterContentDto } from "../../Api/Models";

export const REQUEST_FOOTER_CONTENT = "REQUEST_FOOTER_CONTENT";
export const RECEIVE_FOOTER_CONTENT = "RECEIVE_FOOTER_CONTENT";

export interface IRequestFooterContent { type: typeof REQUEST_FOOTER_CONTENT }
export interface IReceiveFooterContent { type: typeof RECEIVE_FOOTER_CONTENT, payload: IFooterContentDto }

export type TKnownActions = IRequestFooterContent | IReceiveFooterContent | TErrorActions;

export const ActionCreators = 
{
    getFooterContent: (): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {
        if (getState().getFooterContent.content !== combinedDefaults.getFooterContent.content) 
            return;

        dispatch({ type: REQUEST_FOOTER_CONTENT });

        axios.get(GET_FOOTER_CONTENT, 
        {
            method: "GET", 
            responseType: "json"
        })
        .then(response =>
        {
            if (response.status === 200)
            {
                dispatch({ type: RECEIVE_FOOTER_CONTENT, payload: response.data });
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
