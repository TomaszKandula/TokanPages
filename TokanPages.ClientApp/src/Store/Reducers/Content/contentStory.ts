import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { ContentStoryState } from "../../States";

import { TKnownActions, RECEIVE, REQUEST } from "../../Actions/Content/contentStory";

export const ContentStory: Reducer<ContentStoryState> = (
    state: ContentStoryState | undefined,
    incomingAction: Action
): ContentStoryState => {
    if (state === undefined) return ApplicationDefault.contentStory;

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
