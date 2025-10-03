import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { ArticleInfoState } from "../../States";

import { TKnownActions, RECEIVE, REQUEST, CLEAR } from "../../Actions/Articles/articleInfo";

export const ArticleInfo: Reducer<ArticleInfoState> = (
    state: ArticleInfoState | undefined,
    incomingAction: Action
): ArticleInfoState => {
    if (state === undefined) return ApplicationDefault.articleInfo;

    const action = incomingAction as TKnownActions;
    switch (action.type) {
        case REQUEST:
            return {
                isLoading: true,
                info: { 
                    articles: [],
                },
            };

        case RECEIVE:
            return {
                isLoading: false,
                info: {
                    articles: action.payload.articles,
                },
            };

        case CLEAR: 
            return {
                isLoading: false,
                info: {
                    articles: [],
                }
            };

        default:
            return state;
    }
};
