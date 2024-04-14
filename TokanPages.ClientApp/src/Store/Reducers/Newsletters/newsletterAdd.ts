import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { NewsletterAddState } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

import { TKnownActions, ADD, RESPONSE, CLEAR } from "../../Actions/Newsletters/newsletterAdd";

export const NewsletterAdd: Reducer<NewsletterAddState> = (
    state: NewsletterAddState | undefined,
    incomingAction: Action
): NewsletterAddState => {
    if (state === undefined) return ApplicationDefault.newsletterAdd;

    const action = incomingAction as TKnownActions;
    switch (action.type) {
        case CLEAR:
            return ApplicationDefault.newsletterAdd;

        case ADD:
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
