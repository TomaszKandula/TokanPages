import * as Sentry from "@sentry/react";
import { AppThunkAction } from "../applicationState";
import { RAISE_ERROR, TErrorActions } from "./raiseErrorAction";
import { UnexpectedStatusCode } from "../../Shared/textWrappers";
import { GetErrorMessage } from "../../Shared/helpers";
import { SendData } from "../../Api/request";
import { 
    API_COMMAND_UPDATE_ARTICLE_CONTENT, 
    API_COMMAND_UPDATE_ARTICLE_COUNT, 
    API_COMMAND_UPDATE_ARTICLE_LIKES, 
    API_COMMAND_UPDATE_ARTICLE_VISIBILITY 
} from "../../Shared/constants";
import { 
    IUpdateArticleContentDto, 
    IUpdateArticleCountDto, 
    IUpdateArticleLikesDto, 
    IUpdateArticleVisibilityDto 
} from "../../Api/Models";

export const UPDATE_ARTICLE = "UPDATE_ARTICLE";
export const UPDATE_ARTICLE_RESPONSE = "UPDATE_ARTICLE_RESPONSE";
export interface IApiUpdateArticle { type: typeof UPDATE_ARTICLE }
export interface IApiUpdateArticleResponse { type: typeof UPDATE_ARTICLE_RESPONSE, hasUpdatedArticle: boolean }
export type TKnownActions = IApiUpdateArticle | IApiUpdateArticleResponse | TErrorActions;

const DispatchCall = async (dispatch: any, url: string, data: any) =>
{
    dispatch({ type: UPDATE_ARTICLE });

    let result = await SendData(url, data);

    if (result.error !== null)
    {
        dispatch({ type: RAISE_ERROR, errorObject: GetErrorMessage(result.error) });
        Sentry.captureException(result.error);
        return;
    }

    if (result.status === 200)
    {
        dispatch({ type: UPDATE_ARTICLE_RESPONSE, hasUpdatedArticle: true });
        return;
    }

    const error = UnexpectedStatusCode(result.status as number);
    dispatch({ type: RAISE_ERROR, errorObject: error });
    Sentry.captureException(error);
}

export const ActionCreators = 
{
    updateArticleContent: (payload: IUpdateArticleContentDto): AppThunkAction<TKnownActions> => (dispatch) => 
    {
        DispatchCall(dispatch, API_COMMAND_UPDATE_ARTICLE_CONTENT, 
        {  
            id: payload.id,
            title: payload.title,
            description: payload.description,
            textToUpload: payload.textToUpload,
            imageToUpload: payload.imageToUpload
        });
    },
    updateArticleCount: (payload: IUpdateArticleCountDto): AppThunkAction<TKnownActions> => (dispatch) => 
    {
        DispatchCall(dispatch, API_COMMAND_UPDATE_ARTICLE_COUNT, 
        {  
            id: payload.id
        });
        },
    updateArticleLikes: (payload: IUpdateArticleLikesDto): AppThunkAction<TKnownActions> => (dispatch) => 
    {
        DispatchCall(dispatch, API_COMMAND_UPDATE_ARTICLE_LIKES, 
        {  
            id: payload.id,
            addToLikes: payload.addToLikes
        });
    },
    updateArticleVisibility: (payload: IUpdateArticleVisibilityDto): AppThunkAction<TKnownActions> => (dispatch) => 
    {
        DispatchCall(dispatch, API_COMMAND_UPDATE_ARTICLE_VISIBILITY, 
        {  
            id: payload.id,
            isPublished: payload.IsPublished
        });
    }
}
