import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { ContentNewsletterUpdateState } from "../../States";

import { TKnownActions, RECEIVE, REQUEST } from "../../Actions/Content/contentNewsletterUpdate";

export const ContentNewsletterUpdate: Reducer<ContentNewsletterUpdateState> = (
    state: ContentNewsletterUpdateState | undefined,
    incomingAction: Action
): ContentNewsletterUpdateState => {
    if (state === undefined) return ApplicationDefault.contentNewsletterUpdate;

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
