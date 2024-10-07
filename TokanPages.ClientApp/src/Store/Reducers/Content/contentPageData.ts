import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { ContentPageDataState } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

import { TKnownActions, CLEAR, RECEIVE, REQUEST } from "../../Actions/Content/contentPageData";

export const ContentPageData: Reducer<ContentPageDataState> = (
    state: ContentPageDataState | undefined,
    incomingAction: Action
): ContentPageDataState => {
    if (state === undefined) return ApplicationDefault.contentPageData;

    const action = incomingAction as TKnownActions;
    switch (action.type) {
        case CLEAR:
            return {
                status: OperationStatus.notStarted,
                response: {
                    components: []
                },
            };
        case REQUEST:
            return {
                status: OperationStatus.inProgress,
                response: state.response,
            };
        case RECEIVE:
            return {
                status: OperationStatus.hasFinished,
                response: action.payload,
            };
        default:
            return state;
    }
};
