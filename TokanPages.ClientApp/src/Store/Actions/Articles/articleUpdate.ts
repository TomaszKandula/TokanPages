import { ApplicationAction, ApplicationState } from "../../Configuration";
import { useApiAction } from "../../../Shared/Hooks";

import {
    ExecuteStoreActionProps,
    UPDATE_ARTICLE_CONTENT,
    UPDATE_ARTICLE_COUNT,
    UPDATE_ARTICLE_LIKES,
    UPDATE_ARTICLE_VISIBILITY,
} from "../../../Api";

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

const DispatchCall = async (
    dispatch: (action: TKnownActions) => void,
    url: string,
    data: object,
    getState: () => ApplicationState
) => {
    dispatch({ type: UPDATE });

    const actions = useApiAction();
    const input: ExecuteStoreActionProps = {
        url: url,
        dispatch: dispatch,
        state: getState,
        responseType: RESPONSE,
        configuration: {
            method: "POST",
            body: data,
            hasJsonResponse: true,
        },
    };

    actions.storeAction(input);
};

export const ArticleUpdateAction = {
    clear: (): ApplicationAction<TKnownActions> => dispatch => {
        dispatch({ type: CLEAR });
    },
    updateContent:
        (payload: UpdateArticleContentDto): ApplicationAction<TKnownActions> =>
        (dispatch, getState) => {
            DispatchCall(dispatch, UPDATE_ARTICLE_CONTENT, payload, getState);
        },
    updateCount:
        (payload: UpdateArticleCountDto): ApplicationAction<TKnownActions> =>
        (dispatch, getState) => {
            DispatchCall(dispatch, UPDATE_ARTICLE_COUNT, payload, getState);
        },
    updateLikes:
        (payload: UpdateArticleLikesDto): ApplicationAction<TKnownActions> =>
        (dispatch, getState) => {
            DispatchCall(dispatch, UPDATE_ARTICLE_LIKES, payload, getState);
        },
    updateVisibility:
        (payload: UpdateArticleVisibilityDto): ApplicationAction<TKnownActions> =>
        (dispatch, getState) => {
            DispatchCall(dispatch, UPDATE_ARTICLE_VISIBILITY, payload, getState);
        },
};
