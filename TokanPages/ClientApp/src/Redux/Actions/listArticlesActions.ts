import axios from "axios";
import { AppThunkAction } from "../../Redux/applicationState";
import { IArticleItem } from "../../Redux/States/Common/articleItem";
import { API_QUERY_GET_ARTICLES } from "../../Shared/constants";

export const REQUEST_ARTICLES = "REQUEST_ARTICLES";
export const RECEIVE_ARTICLES = "RECEIVE_ARTICLES";

export interface IRequestArticlesAction { type: typeof REQUEST_ARTICLES; }
export interface IReceiveArticlesAction { type: typeof RECEIVE_ARTICLES; payload: IArticleItem[]; }

export type TKnownActions = IRequestArticlesAction | IReceiveArticlesAction;

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
        .then(function (response)
        {              
            if (response.status >= 200 && response.status <= 299)
            {
                dispatch({ type: RECEIVE_ARTICLES, payload: response.data });
            }
            else if (response.status >= 300 && response.status <= 399)
            {
                // handle redirects
            }
        })
        .catch(function (error)
        {
            console.error(error);
            console.error(error.response.data.ErrorMessage);
        });
    }
};
