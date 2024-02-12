import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { ContentActivateAccountState } from "../../States";

import { TKnownActions, REQUEST, RECEIVE } from "../../Actions/Content/contentActivateAccount";

export const ContentActivateAccount: Reducer<ContentActivateAccountState> = (
    state: ContentActivateAccountState | undefined,
    incomingAction: Action
): ContentActivateAccountState => {
    if (state === undefined) return ApplicationDefault.contentActivateAccount;

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
