import axios from "axios";
import { AppThunkAction } from "../applicationState";
import { IUpdateArticleDto } from "../../Api/Models";
import { API_COMMAND_UPDATE_ARTICLE } from "../../Shared/constants";

export const API_UPDATE_ARTICLE = "API_UPDATE_ARTICLE";
export const API_UPDATE_ARTICLE_RESPONSE = "API_UPDATE_ARTICLE_RESPONSE";
export const UPDATE_ARTICLE_ERROR = "UPDATE_ARTICLE_ERROR";

export interface IApiUpdateArticle { type: typeof API_UPDATE_ARTICLE }
export interface IApiUpdateArticleResponse { type: typeof API_UPDATE_ARTICLE_RESPONSE, hasUpdatedArticle: boolean }
export interface IUpdateArticleError { type: typeof UPDATE_ARTICLE_ERROR, errorObject: any }

export type TKnownActions = 
    IApiUpdateArticle | 
    IApiUpdateArticleResponse | 
    IUpdateArticleError
;

export const ActionCreators = 
{
    updateArticle: (payload: IUpdateArticleDto):  AppThunkAction<TKnownActions> => async (dispatch) => 
    {
        dispatch({ type: API_UPDATE_ARTICLE });

        await axios(
        { 
            method: "POST", 
            url: API_COMMAND_UPDATE_ARTICLE, 
            data: 
            { 
                id: payload.id,
                title: payload.title,
                description: payload.description,
                textToUpload: payload.textToUpload,
                imageToUpload: payload.imageToUpload,
                isPublished: payload.isPublished,
                addToLikes: payload.addToLikes,
                upReadCount: payload.upReadCount
            }
        })
        .then(response => 
        {
            return response.status === 200
                ? dispatch({ type: API_UPDATE_ARTICLE_RESPONSE, hasUpdatedArticle: true })
                : dispatch({ type: UPDATE_ARTICLE_ERROR, errorObject: "Unexpected status code" });//TODO: add error object
        })
        .catch(error =>
        {
            dispatch({ type: UPDATE_ARTICLE_ERROR, errorObject: error });
        });     
    }
}
