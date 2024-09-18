import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { ContentBusinessFormState } from "../../States";

import { TKnownActions, REQUEST, RECEIVE } from "../../Actions/Content/contentBusinessForm";

export const ContentBusinessForm: Reducer<ContentBusinessFormState> = (
    state: ContentBusinessFormState | undefined,
    incomingAction: Action
): ContentBusinessFormState => {
    if (state === undefined) return ApplicationDefault.contentBusinessForm;

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
