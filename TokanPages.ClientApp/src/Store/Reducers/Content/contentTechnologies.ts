import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { ContentTechnologiesState } from "../../States";

import { TKnownActions, RECEIVE, REQUEST } from "../../Actions/Content/contentTechnologies";

export const ContentTechnologies: Reducer<ContentTechnologiesState> = (
    state: ContentTechnologiesState | undefined,
    incomingAction: Action
): ContentTechnologiesState => {
    if (state === undefined) return ApplicationDefault.contentTechnologies;

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
