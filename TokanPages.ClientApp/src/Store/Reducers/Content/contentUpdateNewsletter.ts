import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { ContentUpdateNewsletterState } from "../../States";

import { TKnownActions, RECEIVE, REQUEST } from "../../Actions/Content/contentUpdateNewsletter";

export const ContentUpdateNewsletter: Reducer<ContentUpdateNewsletterState> = (
    state: ContentUpdateNewsletterState | undefined,
    incomingAction: Action
): ContentUpdateNewsletterState => {
    if (state === undefined) return ApplicationDefault.contentUpdateNewsletter;

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
