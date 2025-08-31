import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { ArticleListingState } from "../../States";

import { TKnownActions, RECEIVE, REQUEST } from "../../Actions/Articles/articleListing";

export const ArticleListing: Reducer<ArticleListingState> = (
    state: ArticleListingState | undefined,
    incomingAction: Action
): ArticleListingState => {
    if (state === undefined) return ApplicationDefault.articleListing;

    const action = incomingAction as TKnownActions;
    switch (action.type) {
        case REQUEST:
            return {
                isLoading: true,
                payload: state.payload,
            };

        case RECEIVE:
            return {
                isLoading: false,
                payload: action.payload,
            };

        default:
            return state;
    }
};
