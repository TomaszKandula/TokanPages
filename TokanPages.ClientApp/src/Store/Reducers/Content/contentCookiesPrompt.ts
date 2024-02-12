import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { ContentCookiesPromptState } from "../../States";

import { TKnownActions, REQUEST, RECEIVE } from "../../Actions/Content/contentCookiesPrompt";

export const ContentCookiesPrompt: Reducer<ContentCookiesPromptState> = (
    state: ContentCookiesPromptState | undefined,
    incomingAction: Action
): ContentCookiesPromptState => {
    if (state === undefined) return ApplicationDefault.contentCookiesPrompt;

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
