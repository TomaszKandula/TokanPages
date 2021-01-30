import axios from "axios";
import { API_QUERY_GET_ARTICLE, ARTICLE_URL } from "../../Shared/constants";
import { AppThunkAction, IArticleItem } from "../../Redux/applicationState";

export const REQUEST_ARTICLE = "REQUEST_ARTICLE";
export const RECEIVE_ARTICLE = "RECEIVE_ARTICLE";

export interface IRequestArticleAction { type: typeof REQUEST_ARTICLE; }
export interface IReceiveArticleAction { type: typeof RECEIVE_ARTICLE; payload: IArticleItem; }

export type TKnownActions = IRequestArticleAction | IReceiveArticleAction;

export const ActionCreators = 
{
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
                // TODO: handle other statuses
            }
        }))
        .catch(error => 
        {
            console.error(error);
            console.error(error.response.data.ErrorMessage);
        });
    }
};
