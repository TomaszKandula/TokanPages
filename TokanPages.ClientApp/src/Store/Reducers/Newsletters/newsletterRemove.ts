import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { NewsletterRemoveState } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

import { TKnownActions, REMOVE, RESPONSE } from "../../Actions/Newsletters/newsletterRemove";

export const NewsletterRemove: Reducer<NewsletterRemoveState> = (
    state: NewsletterRemoveState | undefined,
    incomingAction: Action
): NewsletterRemoveState => {
    if (state === undefined) return ApplicationDefault.newsletterRemove;

    const action = incomingAction as TKnownActions;
    switch (action.type) {
        case REMOVE:
            return {
                status: OperationStatus.inProgress,
                response: state.response,
            };

        case RESPONSE:
            return {
                status: OperationStatus.hasFinished,
                response: action.payload,
            };

        default:
            return state;
    }
};
