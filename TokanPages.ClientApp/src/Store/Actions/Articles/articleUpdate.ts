import { IApplicationAction } from "../../Configuration";
import { RAISE_ERROR } from "../Application/applicationError";
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
export interface IUpdateArticle { type: typeof UPDATE_ARTICLE }
export interface IUpdateArticleClear { type: typeof UPDATE_ARTICLE_CLEAR }
export interface IUpdateArticleResponse { type: typeof UPDATE_ARTICLE_RESPONSE }
export type TKnownActions = IUpdateArticle | IUpdateArticleClear | IUpdateArticleResponse;

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

export const ArticleUpdateAction = 
{
    clear: (): IApplicationAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: UPDATE_ARTICLE_CLEAR });
    },
    updateContent: (payload: IUpdateArticleContentDto): IApplicationAction<TKnownActions> => (dispatch) => 
    {
        DispatchCall(dispatch, API_COMMAND_UPDATE_ARTICLE_CONTENT, payload);
    },
    updateCount: (payload: IUpdateArticleCountDto): IApplicationAction<TKnownActions> => (dispatch) => 
    {
        DispatchCall(dispatch, API_COMMAND_UPDATE_ARTICLE_COUNT, payload);
    },
    updateLikes: (payload: IUpdateArticleLikesDto): IApplicationAction<TKnownActions> => (dispatch) => 
    {
        DispatchCall(dispatch, API_COMMAND_UPDATE_ARTICLE_LIKES, payload);
    },
    updateVisibility: (payload: IUpdateArticleVisibilityDto): IApplicationAction<TKnownActions> => (dispatch) => 
    {
        DispatchCall(dispatch, API_COMMAND_UPDATE_ARTICLE_VISIBILITY, payload);
    }
}
