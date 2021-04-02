import axios from "axios";
import { AppThunkAction } from "../applicationState";
import { IUpdateArticleDto } from "../../Api/Models";
import { API_COMMAND_UPDATE_ARTICLE } from "../../Shared/constants";

export const API_UPDATE_ARTICLE = "API_UPDATE_ARTICLE";
export const API_UPDATE_ARTICLE_RESPONSE = "API_UPDATE_ARTICLE_RESPONSE";

export interface IApiUpdateArticle { type: typeof API_UPDATE_ARTICLE }
export interface IApiUpdateArticleResponse { type: typeof API_UPDATE_ARTICLE_RESPONSE, hasUpdatedArticle: boolean }

export type TKnownActions = IApiUpdateArticle | IApiUpdateArticleResponse;

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
        .then(function (response) 
        {
            if (response.status === 200)
            {
                dispatch({ type: API_UPDATE_ARTICLE_RESPONSE, hasUpdatedArticle: true });
            }
            else
            {
                dispatch({ type: API_UPDATE_ARTICLE_RESPONSE, hasUpdatedArticle: false });
                console.log(response.status); //TODO: add proper status code handling
            }
        })
        .catch(function (error) 
        {
            dispatch({ type: API_UPDATE_ARTICLE_RESPONSE, hasUpdatedArticle: false });
            console.log(error); //TODO: add proper error handling
        });     
    }
}
