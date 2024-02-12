import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { ContentClientsState } from "../../States";

import { TKnownActions, REQUEST, RECEIVE } from "../../Actions/Content/contentClients";

export const ContentClients: Reducer<ContentClientsState> = (
    state: ContentClientsState | undefined,
    incomingAction: Action
): ContentClientsState => {
    if (state === undefined) return ApplicationDefault.contentClients;

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
