import { AppThunkAction, IArticle } from "Redux/applicationState";
import { API_QUERY_GET_ARTICLES } from "Shared/constants";
import axios from "axios";

export const REQUEST_ARTICLES = "REQUEST_ARTICLES";
export const RECEIVE_ARTICLES = "RECEIVE_ARTICLES";

export interface IRequestArticlesAction { type: typeof REQUEST_ARTICLES; }
export interface IReceiveArticlesAction { type: typeof RECEIVE_ARTICLES; payload: IArticle[]; }

export type TKnownActions = IRequestArticlesAction | IReceiveArticlesAction;

export const ActionCreators = 
{

    requestArticles: (): AppThunkAction<TKnownActions> => (dispatch) =>
    {

        dispatch({ type: REQUEST_ARTICLES });

        axios.get(API_QUERY_GET_ARTICLES, {method: "get", responseType: "json"})
        .then(function (response)
        {
               
            if (response.status === 200)
            {
                dispatch({ type: RECEIVE_ARTICLES, payload: response.data });
            }
            else
            {
                console.error(response.data.ErrorMessage);   
            }

        })
        .catch(function (error)
        {
            console.error(error);
        });

    }

};
