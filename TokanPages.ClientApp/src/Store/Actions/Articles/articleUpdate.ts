import { ApplicationAction } from "../../Configuration";
import { RaiseError } from "../../../Shared/Services/ErrorServices";

import {
    ExecuteAsync,
    UPDATE_ARTICLE_CONTENT,
    UPDATE_ARTICLE_COUNT,
    UPDATE_ARTICLE_LIKES,
    UPDATE_ARTICLE_VISIBILITY,
} from "../../../Api/Request";

import {
    ApplicationProps,
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

const DispatchCall = async (
    dispatch: (action: TKnownActions) => void,
    url: string,
    data: object,
    content: ApplicationProps
) => {
    dispatch({ type: UPDATE });

    let result = await ExecuteAsync({
        url: url,
        method: "POST",
        responseType: "json",
        data: data,
    });

    if (result.error !== null) {
        RaiseError({
            dispatch: dispatch,
            errorObject: result.error,
            content: content,
        });

        return;
    }

    if (result.status === 200) {
        dispatch({ type: RESPONSE, payload: result.content });
        dispatch({ type: CLEAR });
        return;
    }

    const statusCode = result.status as number;
    const statusText = content.unexpectedStatus.replace("{STATUS_CODE}", statusCode.toString());
    RaiseError({
        dispatch: dispatch,
        errorObject: statusText,
        content: content,
    });
};

export const ArticleUpdateAction = {
    clear: (): ApplicationAction<TKnownActions> => dispatch => {
        dispatch({ type: CLEAR });
    },
    updateContent:
        (payload: UpdateArticleContentDto): ApplicationAction<TKnownActions> =>
        (dispatch, getState) => {
            const content = getState().contentTemplates.content.templates.application;
            DispatchCall(dispatch, UPDATE_ARTICLE_CONTENT, payload, content);
        },
    updateCount:
        (payload: UpdateArticleCountDto): ApplicationAction<TKnownActions> =>
        (dispatch, getState) => {
            const content = getState().contentTemplates.content.templates.application;
            DispatchCall(dispatch, UPDATE_ARTICLE_COUNT, payload, content);
        },
    updateLikes:
        (payload: UpdateArticleLikesDto): ApplicationAction<TKnownActions> =>
        (dispatch, getState) => {
            const content = getState().contentTemplates.content.templates.application;
            DispatchCall(dispatch, UPDATE_ARTICLE_LIKES, payload, content);
        },
    updateVisibility:
        (payload: UpdateArticleVisibilityDto): ApplicationAction<TKnownActions> =>
        (dispatch, getState) => {
            const content = getState().contentTemplates.content.templates.application;
            DispatchCall(dispatch, UPDATE_ARTICLE_VISIBILITY, payload, content);
        },
};
