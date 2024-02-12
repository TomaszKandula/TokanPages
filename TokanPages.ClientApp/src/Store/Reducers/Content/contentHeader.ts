import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { ContentHeaderState } from "../../States";

import { TKnownActions, RECEIVE, REQUEST } from "../../Actions/Content/contentHeader";

export const ContentHeader: Reducer<ContentHeaderState> = (
    state: ContentHeaderState | undefined,
    incomingAction: Action
): ContentHeaderState => {
    if (state === undefined) return ApplicationDefault.contentHeader;

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
