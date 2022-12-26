import { IApplicationAction } from "../../Configuration";
import { RAISE } from "../Application/applicationError";
import { GetTextStatusCode } from "../../../Shared/Services/Utilities";
import { GetErrorMessage } from "../../../Shared/Services/ErrorServices";

import { 
    ApiCall, 
    EnrichConfiguration, 
    UPDATE_ARTICLE_CONTENT, 
    UPDATE_ARTICLE_COUNT, 
    UPDATE_ARTICLE_LIKES, 
    UPDATE_ARTICLE_VISIBILITY
} from "../../../Api/Request";

import { 
    IUpdateArticleContentDto, 
    IUpdateArticleCountDto, 
    IUpdateArticleLikesDto, 
    IUpdateArticleVisibilityDto 
} from "../../../Api/Models";

export const UPDATE = "UPDATE_ARTICLE";
export const CLEAR = "UPDATE_ARTICLE_CLEAR";
export const RESPONSE = "UPDATE_ARTICLE_RESPONSE";
interface IUpdate { type: typeof UPDATE }
interface IClear { type: typeof CLEAR }
interface IResponse { type: typeof RESPONSE; payload: any; }
export type TKnownActions = IUpdate | IClear | IResponse;

const DispatchCall = async (dispatch: any, url: string, data: any) =>
{
    dispatch({ type: UPDATE });

    let result = await ApiCall(EnrichConfiguration(
    {
        url: url,
        method: "POST",
        responseType: "json",
        data: data
    }));

    if (result.error !== null)
    {
        dispatch({ type: RAISE, errorObject: GetErrorMessage({ errorObject: result.error }) });
        return;
    }

    if (result.status === 200)
    {
        dispatch({ type: RESPONSE, payload: result.content });
        dispatch({ type: CLEAR });
        return;
    }

    const error = GetTextStatusCode({ statusCode: result.status as number });
    dispatch({ type: RAISE, errorObject: error });
}

export const ArticleUpdateAction = 
{
    clear: (): IApplicationAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: CLEAR });
    },
    updateContent: (payload: IUpdateArticleContentDto): IApplicationAction<TKnownActions> => (dispatch) => 
    {
        DispatchCall(dispatch, UPDATE_ARTICLE_CONTENT, payload);
    },
    updateCount: (payload: IUpdateArticleCountDto): IApplicationAction<TKnownActions> => (dispatch) => 
    {
        DispatchCall(dispatch, UPDATE_ARTICLE_COUNT, payload);
    },
    updateLikes: (payload: IUpdateArticleLikesDto): IApplicationAction<TKnownActions> => (dispatch) => 
    {
        DispatchCall(dispatch, UPDATE_ARTICLE_LIKES, payload);
    },
    updateVisibility: (payload: IUpdateArticleVisibilityDto): IApplicationAction<TKnownActions> => (dispatch) => 
    {
        DispatchCall(dispatch, UPDATE_ARTICLE_VISIBILITY, payload);
    }
}
