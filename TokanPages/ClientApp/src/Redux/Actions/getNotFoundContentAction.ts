import axios from "axios";
import * as Sentry from "@sentry/react";
import { AppThunkAction } from "../applicationState";
import { GetErrorMessage } from "../../Shared/helpers";
import { UnexpectedStatusCode } from "../../Shared/textWrappers";
import { GET_NOTFOUND_CONTENT } from "../../Shared/constants";
import { RAISE_ERROR, TErrorActions } from "./raiseErrorAction";
import { INotFoundContentDto } from "../../Api/Models";

export const REQUEST_NOTFOUND_CONTENT = "REQUEST_NOTFOUND_CONTENT";
export const RECEIVE_NOTFOUND_CONTENT = "RECEIVE_NOTFOUND_CONTENT";

export interface IRequestNotFoundContent { type: typeof REQUEST_NOTFOUND_CONTENT }
export interface IReceiveNotFoundContent { type: typeof RECEIVE_NOTFOUND_CONTENT, payload: INotFoundContentDto }

export type TKnownActions = IRequestNotFoundContent | IReceiveNotFoundContent | TErrorActions;

export const ActionCreators = 
{
    getNotFoundContent: (): AppThunkAction<TKnownActions> => (dispatch) =>
    {
        dispatch({ type: REQUEST_NOTFOUND_CONTENT });

        axios.get(GET_NOTFOUND_CONTENT, 
        {
            method: "GET", 
            responseType: "json"
        })
        .then(response =>
        {
            if (response.status === 200)
            {
                dispatch({ type: RECEIVE_NOTFOUND_CONTENT, payload: response.data });
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