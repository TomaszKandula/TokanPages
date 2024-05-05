import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { ContentArticleState } from "../../States";

import { TKnownActions, RECEIVE, REQUEST } from "../../Actions/Content/contentArticle";

export const ContentArticle: Reducer<ContentArticleState> = (
    state: ContentArticleState | undefined,
    incomingAction: Action
): ContentArticleState => {
    if (state === undefined) return ApplicationDefault.contentArticle;

    const action = incomingAction as TKnownActions;
    switch (action.type) {
        case REQUEST:
            return {
                isLoading: true,
                content: state.content,
            };

        case RECEIVE:
            return {
                isLoading: false,
                content: action.payload.content,
            };

        default:
            return state;
    }
};
