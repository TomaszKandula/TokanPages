import { AppThunkAction, IArticle } from "Redux/applicationState";
import { API_GET_ARTICLES } from "Shared/constants";
import axios from "axios";

export const REQUEST_ARTICLES = "REQUEST_ARTICLES";
export const RECEIVE_ARTICLES = "RECEIVE_ARTICLES";

export interface IRequestArticlesAction { type: typeof REQUEST_ARTICLES; }
export interface IReceiveArticlesAction { type: typeof RECEIVE_ARTICLES; payload: IArticle[]; }

export type TKnownActions = IRequestArticlesAction | IReceiveArticlesAction;

export const actionCreators = 
{

    requestArticles: (): AppThunkAction<TKnownActions> => (dispatch) =>
    {

        dispatch({ type: REQUEST_ARTICLES });

        axios.get(API_GET_ARTICLES, {method: "get", responseType: "json"})
        .then(function (response)
        {
               
            if (response.status === 200 && response.data.error.errorCode === "no_errors_found")
            {
                dispatch({ type: RECEIVE_ARTICLES, payload: response.data.articles });
            }
            else
            {
                console.error(response.data.error.errorDesc);   
            }

        })
        .catch(function (error)
        {
            console.error(error);
        });

    }

};
