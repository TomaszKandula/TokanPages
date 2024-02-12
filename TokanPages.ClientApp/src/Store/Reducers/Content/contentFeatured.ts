import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { ContentFeaturedState } from "../../States";

import { TKnownActions, RECEIVE, REQUEST } from "../../Actions/Content/contentFeatured";

export const ContentFeatured: Reducer<ContentFeaturedState> = (
    state: ContentFeaturedState | undefined,
    incomingAction: Action
): ContentFeaturedState => {
    if (state === undefined) return ApplicationDefault.contentFeatured;

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
