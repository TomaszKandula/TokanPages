import axios from "axios";
import { AppThunkAction } from "../../Redux/applicationState";
import { IArticleItem } from "../../Shared/ContentRender/Models/articleItemModel";
import { API_QUERY_GET_ARTICLES } from "../../Shared/constants";

export const REQUEST_ARTICLES = "REQUEST_ARTICLES";
export const RECEIVE_ARTICLES = "RECEIVE_ARTICLES";
export const REQUEST_ARTICLES_ERROR = "REQUEST_ARTICLES_ERROR";

export interface IRequestArticlesAction { type: typeof REQUEST_ARTICLES; }
export interface IReceiveArticlesAction { type: typeof RECEIVE_ARTICLES; payload: IArticleItem[]; }
export interface IRequestArticlesError { type: typeof REQUEST_ARTICLES_ERROR, errorObject: any }

export type TKnownActions = 
    IRequestArticlesAction | 
    IReceiveArticlesAction | 
    IRequestArticlesError
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
                : dispatch({ type: REQUEST_ARTICLES_ERROR, errorObject: "Unexpected status code" });//TODO: add error object

        })
        .catch(error =>
        {
            dispatch({ type: REQUEST_ARTICLES_ERROR, errorObject: error });
        });
    }
};
