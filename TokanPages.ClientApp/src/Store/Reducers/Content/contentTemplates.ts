import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { ContentTemplatesState } from "../../States";

import { TKnownActions, RECEIVE, REQUEST } from "../../Actions/Content/contentTemplates";

export const ContentTemplates: Reducer<ContentTemplatesState> = (
    state: ContentTemplatesState | undefined,
    incomingAction: Action
): ContentTemplatesState => {
    if (state === undefined) return ApplicationDefault.contentTemplates;

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
