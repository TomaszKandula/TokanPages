import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { ContentNewsletterRemoveState } from "../../States";

import { TKnownActions, RECEIVE, REQUEST } from "../../Actions/Content/contentNewsletterRemove";

export const ContentNewsletterRemove: Reducer<ContentNewsletterRemoveState> = (
    state: ContentNewsletterRemoveState | undefined,
    incomingAction: Action
): ContentNewsletterRemoveState => {
    if (state === undefined) return ApplicationDefault.contentNewsletterRemove;

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
