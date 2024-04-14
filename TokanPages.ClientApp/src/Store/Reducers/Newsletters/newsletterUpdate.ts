import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { NewsletterUpdateState } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

import { TKnownActions, UPDATE, RESPONSE } from "../../Actions/Newsletters/newsletterUpdate";

export const NewsletterUpdate: Reducer<NewsletterUpdateState> = (
    state: NewsletterUpdateState | undefined,
    incomingAction: Action
): NewsletterUpdateState => {
    if (state === undefined) return ApplicationDefault.newsletterUpdate;

    const action = incomingAction as TKnownActions;
    switch (action.type) {
        case UPDATE:
            return {
                status: OperationStatus.notStarted,
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
