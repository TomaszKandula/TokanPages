import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { ContentContactFormState } from "../../States";

import { TKnownActions, REQUEST, RECEIVE } from "../../Actions/Content/contentContactForm";

export const ContentContactForm: Reducer<ContentContactFormState> = (
    state: ContentContactFormState | undefined,
    incomingAction: Action
): ContentContactFormState => {
    if (state === undefined) return ApplicationDefault.contentContactForm;

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
