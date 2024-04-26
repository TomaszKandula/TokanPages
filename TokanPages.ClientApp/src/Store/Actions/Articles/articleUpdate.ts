import { ApplicationAction } from "../../Configuration";
import { RAISE } from "../Application/applicationError";
import { GetTextStatusCode } from "../../../Shared/Services/Utilities";
import { GetErrorMessage } from "../../../Shared/Services/ErrorServices";

import {
    ExecuteAsync,
    UPDATE_ARTICLE_CONTENT,
    UPDATE_ARTICLE_COUNT,
    UPDATE_ARTICLE_LIKES,
    UPDATE_ARTICLE_VISIBILITY,
} from "../../../Api/Request";

import {
    UpdateArticleContentDto,
    UpdateArticleCountDto,
    UpdateArticleLikesDto,
    UpdateArticleVisibilityDto,
} from "../../../Api/Models";

export const UPDATE = "UPDATE_ARTICLE";
export const CLEAR = "UPDATE_ARTICLE_CLEAR";
export const RESPONSE = "UPDATE_ARTICLE_RESPONSE";
interface Update {
    type: typeof UPDATE;
}
interface Clear {
    type: typeof CLEAR;
}
interface Response {
    type: typeof RESPONSE;
    payload: any;
}
export type TKnownActions = Update | Clear | Response;
//TODO: add types
const DispatchCall = async (dispatch: any, url: string, data: any) => {
    dispatch({ type: UPDATE });

    let result = await ExecuteAsync({
        url: url,
        method: "POST",
        responseType: "json",
        data: data,
    });

    if (result.error !== null) {
        dispatch({ type: RAISE, errorObject: GetErrorMessage({ errorObject: result.error }) });
        return;
    }

    if (result.status === 200) {
        dispatch({ type: RESPONSE, payload: result.content });
        dispatch({ type: CLEAR });
        return;
    }

    const error = GetTextStatusCode({ statusCode: result.status as number });
    dispatch({ type: RAISE, errorObject: error });
};

export const ArticleUpdateAction = {
    clear: (): ApplicationAction<TKnownActions> => dispatch => {
        dispatch({ type: CLEAR });
    },
    updateContent:
        (payload: UpdateArticleContentDto): ApplicationAction<TKnownActions> =>
        dispatch => {
            DispatchCall(dispatch, UPDATE_ARTICLE_CONTENT, payload);
        },
    updateCount:
        (payload: UpdateArticleCountDto): ApplicationAction<TKnownActions> =>
        dispatch => {
            DispatchCall(dispatch, UPDATE_ARTICLE_COUNT, payload);
        },
    updateLikes:
        (payload: UpdateArticleLikesDto): ApplicationAction<TKnownActions> =>
        dispatch => {
            DispatchCall(dispatch, UPDATE_ARTICLE_LIKES, payload);
        },
    updateVisibility:
        (payload: UpdateArticleVisibilityDto): ApplicationAction<TKnownActions> =>
        dispatch => {
            DispatchCall(dispatch, UPDATE_ARTICLE_VISIBILITY, payload);
        },
};
