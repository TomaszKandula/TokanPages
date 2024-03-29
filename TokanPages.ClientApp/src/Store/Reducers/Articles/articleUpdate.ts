import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { ArticleUpdateState } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

import { TKnownActions, UPDATE, CLEAR, RESPONSE } from "../../Actions/Articles/articleUpdate";

export const ArticleUpdate: Reducer<ArticleUpdateState> = (
    state: ArticleUpdateState | undefined,
    incomingAction: Action
): ArticleUpdateState => {
    if (state === undefined) return ApplicationDefault.articleUpdate;

    const action = incomingAction as TKnownActions;
    switch (action.type) {
        case CLEAR:
            return ApplicationDefault.articleUpdate;

        case UPDATE:
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
