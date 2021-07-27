import axios from "axios";
import { AppThunkAction } from "../applicationState";
import { IArticleItem } from "../../Shared/Components/ContentRender/Models/articleItemModel";
import { API_QUERY_GET_ARTICLE, ARTICLE_URL, NULL_RESPONSE_ERROR } from "../../Shared/constants";
import { TErrorActions } from "./raiseErrorAction";
import { UnexpectedStatusCode } from "../../Shared/textWrappers";
import { RaiseError } from "../../Shared/helpers";

export const RESET_SELECTION = "RESET_SELECTION";
export const REQUEST_ARTICLE = "REQUEST_ARTICLE";
export const RECEIVE_ARTICLE = "RECEIVE_ARTICLE";
export interface IResetSelection { type: typeof RESET_SELECTION; }
export interface IRequestArticleAction { type: typeof REQUEST_ARTICLE; }
export interface IReceiveArticleAction { type: typeof RECEIVE_ARTICLE; payload: IArticleItem; }
export type TKnownActions = IResetSelection | IRequestArticleAction | IReceiveArticleAction | TErrorActions;

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
        const requestDetails = axios(artcileDetailsUrl, { method: "GET", responseType: "json" });
        const requestText = axios(articleTextUrl, { method: "GET", responseType: "json" });  

        axios.all([requestDetails, requestText]).then(axios.spread((...responses) => 
        {
            const detailsResponse = responses[0];
            const textResponse = responses[1];

            if (detailsResponse.status === 200 && textResponse.status === 200)
            {
                if (detailsResponse.data === null)
                    return RaiseError(dispatch, NULL_RESPONSE_ERROR);

                if (textResponse.data === null)
                    return RaiseError(dispatch, NULL_RESPONSE_ERROR);
                
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

                return dispatch({ type: RECEIVE_ARTICLE, payload: combineData });
            }
            else
            {
                if (detailsResponse.status !== 200) 
                    return RaiseError(dispatch, UnexpectedStatusCode(detailsResponse.status));

                if (textResponse.status !== 200) 
                    return RaiseError(dispatch, UnexpectedStatusCode(textResponse.status));
            }
        }))
        .catch(error => 
        {
            RaiseError(dispatch, error);
        });
    }
};