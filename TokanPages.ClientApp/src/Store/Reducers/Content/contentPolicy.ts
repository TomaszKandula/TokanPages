import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { ContentPolicyState } from "../../States";

import { TKnownActions, RECEIVE, REQUEST } from "../../Actions/Content/contentPolicy";

export const ContentPolicy: Reducer<ContentPolicyState> = (
    state: ContentPolicyState | undefined,
    incomingAction: Action
): ContentPolicyState => {
    if (state === undefined) return ApplicationDefault.contentPolicy;

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
