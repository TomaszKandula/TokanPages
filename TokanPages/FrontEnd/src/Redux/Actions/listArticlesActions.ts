import { AppThunkAction } from "Redux/applicationState";
import { IListArticles } from "Redux/State/listArticlesState";
import { API_GET_ARTICLES } from "../../Shared/constants";
import axios from "axios";

export const REQUEST_ARTICLES = "REQUEST_ARTICLES";
export const RECEIVE_ARTICLES = "RECEIVE_ARTICLES";

export interface IRequestArticles 
{ 
    type: typeof REQUEST_ARTICLES 
}

export interface IReveiveArticles 
{ 
    type: typeof RECEIVE_ARTICLES,
    data: IListArticles[];
}

export type TKnownActions = IRequestArticles | IReveiveArticles;

export const actionCreators =
{

    requestArticles: (articles: IListArticles[]): AppThunkAction<TKnownActions> => (dispatch, getState) =>
    {

        const appState = getState();
        if (appState && appState.listArticles && articles !== appState.listArticles.articles)
        {

            axios.get(API_GET_ARTICLES, {method: "get", responseType: "json"})
            .then(function (response)
            {

                if (response.status === 200 && response.data.isSucceeded)
                {
                    dispatch({ type: RECEIVE_ARTICLES, data: response.data });
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

            dispatch({ type: REQUEST_ARTICLES });

        }

    }

};
