import axios from "axios";
import { AppThunkAction } from "../applicationState";
import { IArticleItem } from "../../Shared/Components/ContentRender/Models/articleItemModel";
import { API_QUERY_GET_ARTICLES } from "../../Shared/constants";
import { RAISE_ERROR, TErrorActions } from "./raiseErrorAction";
import { GetUnexpectedStatusCode } from "../../Shared/messageHelper";

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

        axios.get(API_QUERY_GET_ARTICLES, 
        {
            method: "GET", 
            responseType: "json"
        })
        .then(response =>
        {
            return response.status === 200
                ? dispatch({ type: RECEIVE_ARTICLES, payload: response.data })
                : dispatch({ type: RAISE_ERROR, errorObject: GetUnexpectedStatusCode(response.status) });

        })
        .catch(error =>
        {
            dispatch({ type: RAISE_ERROR, errorObject: error });
        });
    }
};
