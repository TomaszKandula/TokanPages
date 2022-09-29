import { AppThunkAction } from "../../Configuration";
import { RAISE_ERROR, TErrorActions } from "../raiseErrorAction";
import { GetTextStatusCode } from "../../../Shared/Services/Utilities";
import { GetErrorMessage } from "../../../Shared/Services/ErrorServices";
import { ApiCall, EnrichConfiguration } from "../../../Api/Request";
import { 
    API_COMMAND_UPDATE_ARTICLE_CONTENT, 
    API_COMMAND_UPDATE_ARTICLE_COUNT, 
    API_COMMAND_UPDATE_ARTICLE_LIKES, 
    API_COMMAND_UPDATE_ARTICLE_VISIBILITY
} from "../../../Shared/constants";
import { 
    IUpdateArticleContentDto, 
    IUpdateArticleCountDto, 
    IUpdateArticleLikesDto, 
    IUpdateArticleVisibilityDto 
} from "../../../Api/Models";

export const UPDATE_ARTICLE = "UPDATE_ARTICLE";
export const UPDATE_ARTICLE_CLEAR = "UPDATE_ARTICLE_CLEAR";
export const UPDATE_ARTICLE_RESPONSE = "UPDATE_ARTICLE_RESPONSE";
export interface IApiUpdateArticle { type: typeof UPDATE_ARTICLE }
export interface IApiUpdateArticleClear { type: typeof UPDATE_ARTICLE_CLEAR }
export interface IApiUpdateArticleResponse { type: typeof UPDATE_ARTICLE_RESPONSE }
export type TKnownActions = IApiUpdateArticle | IApiUpdateArticleClear | IApiUpdateArticleResponse | TErrorActions;

const DispatchCall = async (dispatch: any, url: string, data: any) =>
{
    dispatch({ type: UPDATE_ARTICLE });

    let result = await ApiCall(EnrichConfiguration(
    {
        url: url,
        method: "POST",
        responseType: "json",
        data: data
    }));

    if (result.error !== null)
    {
        dispatch({ type: RAISE_ERROR, errorObject: GetErrorMessage({ errorObject: result.error }) });
        return;
    }

    if (result.status === 200)
    {
        dispatch({ type: UPDATE_ARTICLE_RESPONSE });
        dispatch({ type: UPDATE_ARTICLE_CLEAR });
        return;
    }

    const error = GetTextStatusCode({ statusCode: result.status as number });
    dispatch({ type: RAISE_ERROR, errorObject: error });
}

export const ActionCreators = 
{
    updateArticleClear: (): AppThunkAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: UPDATE_ARTICLE_CLEAR });
    },
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
