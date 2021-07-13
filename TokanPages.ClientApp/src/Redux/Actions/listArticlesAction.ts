import * as Sentry from "@sentry/react";
import axios from "axios";
import { AppThunkAction } from "../applicationState";
import { IArticleItem } from "../../Shared/Components/ContentRender/Models/articleItemModel";
import { API_QUERY_GET_ARTICLES } from "../../Shared/constants";
import { RAISE_ERROR, TErrorActions } from "./raiseErrorAction";
import { UnexpectedStatusCode } from "../../Shared/textWrappers";
import { GetErrorMessage } from "../../Shared/helpers";

export const REQUEST_ARTICLES = "REQUEST_ARTICLES";
export const RECEIVE_ARTICLES = "RECEIVE_ARTICLES";

export interface IRequestArticlesAction { type: typeof REQUEST_ARTICLES; }
export interface IReceiveArticlesAction { type: typeof RECEIVE_ARTICLES; payload: IArticleItem[]; }

export type TKnownActions = 
    IRequestArticlesAction | 
    IReceiveArticlesAction | 
    TErrorActions
;

export const ActionCreators = 
{
    requestArticles: (): AppThunkAction<TKnownActions> => (dispatch) =>
    {
        dispatch({ type: REQUEST_ARTICLES });

        axios( 
        {
            method: "GET", 
            url: API_QUERY_GET_ARTICLES,
            responseType: "json"
        })
        .then(response =>
        {
            if (response.status === 200)
            {
                dispatch({ type: RECEIVE_ARTICLES, payload: response.data });
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
};
