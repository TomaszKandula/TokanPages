import axios from "axios";
import { AppThunkAction } from "../applicationState";
import { IUpdateArticleDto } from "../../Api/Models";
import { API_COMMAND_UPDATE_ARTICLE } from "../../Shared/constants";
import { RAISE_ERROR, TErrorActions } from "./raiseErrorAction";
import { UnexpectedStatusCode } from "../../Shared/textWrappers";
import { GetErrorMessage } from "../../Shared/helpers";

export const UPDATE_ARTICLE = "UPDATE_ARTICLE";
export const UPDATE_ARTICLE_RESPONSE = "UPDATE_ARTICLE_RESPONSE";

export interface IApiUpdateArticle { type: typeof UPDATE_ARTICLE }
export interface IApiUpdateArticleResponse { type: typeof UPDATE_ARTICLE_RESPONSE, hasUpdatedArticle: boolean }

export type TKnownActions = 
    IApiUpdateArticle | 
    IApiUpdateArticleResponse | 
    TErrorActions
;

export const ActionCreators = 
{
    updateArticle: (payload: IUpdateArticleDto):  AppThunkAction<TKnownActions> => async (dispatch) => 
    {
        dispatch({ type: UPDATE_ARTICLE });

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
                ? dispatch({ type: UPDATE_ARTICLE_RESPONSE, hasUpdatedArticle: true })
                : dispatch({ type: RAISE_ERROR, errorObject: UnexpectedStatusCode(response.status) });
        })
        .catch(error =>
        {
            dispatch({ type: RAISE_ERROR, errorObject: GetErrorMessage(error) });
        });     
    }
}
