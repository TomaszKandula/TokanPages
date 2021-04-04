import axios from "axios";
import { AppThunkAction } from "../../Redux/applicationState";
import { IArticleItem } from "../../Shared/ContentRender/Models/articleItemModel";
import { API_QUERY_GET_ARTICLE, ARTICLE_URL } from "../../Shared/constants";

export const RESET_SELECTION = "RESET_SELECTION";
export const REQUEST_ARTICLE = "REQUEST_ARTICLE";
export const RECEIVE_ARTICLE = "RECEIVE_ARTICLE";
export const REQUEST_ARTICLE_ERROR = "REQUEST_ARTICLE_ERROR";

export interface IResetSelection { type: typeof RESET_SELECTION; }
export interface IRequestArticleAction { type: typeof REQUEST_ARTICLE; }
export interface IReceiveArticleAction { type: typeof RECEIVE_ARTICLE; payload: IArticleItem; }
export interface IRequestArticleError { type: typeof REQUEST_ARTICLE_ERROR, errorObject: any }

export type TKnownActions = 
    IResetSelection | 
    IRequestArticleAction | 
    IReceiveArticleAction | 
    IRequestArticleError
;

export const ActionCreators = 
{
    resetSelection: (): AppThunkAction<TKnownActions> => (dispatch) =>
    {
        dispatch({ type: RESET_SELECTION });
    },   
    selectArticle: (id: string): AppThunkAction<TKnownActions> => (dispatch) =>
    {
        dispatch({ type: REQUEST_ARTICLE });

        const artcileDetailsUrl = API_QUERY_GET_ARTICLE.replace("{id}", id);
        const articleTextUrl = ARTICLE_URL.replace("{ID}", id);
        const requestDetails = axios.get(artcileDetailsUrl, { method: "GET", responseType: "json" });
        const requestText = axios.get(articleTextUrl, { method: "GET", responseType: "json" });  

        axios.all([requestDetails, requestText]).then(axios.spread((...responses) => 
        {
            const detailsResponse = responses[0];
            const textResponse = responses[1];

            if (detailsResponse.status === 200 && textResponse.status === 200)
            {
                const combineData: IArticleItem = 
                {
                    id: detailsResponse.data.id,
                    title: detailsResponse.data.title,
                    description: detailsResponse.data.description,
                    isPublished: detailsResponse.data.isPublished,
                    likeCount: detailsResponse.data.likeCount,
                    userLikes: detailsResponse.data.userLikes,
                    readCount: detailsResponse.data.readCount,
                    createdAt: detailsResponse.data.createdAt,
                    updatedAt: detailsResponse.data.updatedAt,
                    author: detailsResponse.data.author,
                    text: textResponse.data.items
                };
                dispatch({ type: RECEIVE_ARTICLE, payload: combineData });
            }
            else
            {
                dispatch({ type: REQUEST_ARTICLE_ERROR, errorObject: "Unexpected status code" });//TODO: add object error
            }
        }))
        .catch(error => 
        {
            dispatch({ type: REQUEST_ARTICLE_ERROR, errorObject: error });
        });
    }
};
