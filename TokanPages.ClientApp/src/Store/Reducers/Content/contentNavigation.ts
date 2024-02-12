import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { ContentNavigationState } from "../../States";

import { TKnownActions, RECEIVE, REQUEST } from "../../Actions/Content/contentNavigation";

export const ContentNavigation: Reducer<ContentNavigationState> = (
    state: ContentNavigationState | undefined,
    incomingAction: Action
): ContentNavigationState => {
    if (state === undefined) return ApplicationDefault.contentNavigation;

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
