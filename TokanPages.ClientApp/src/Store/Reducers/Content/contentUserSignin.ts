import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { ContentUserSigninState } from "../../States";

import { TKnownActions, REQUEST, RECEIVE } from "../../Actions/Content/contentUserSignin";

export const ContentUserSignin: Reducer<ContentUserSigninState> = (
    state: ContentUserSigninState | undefined,
    incomingAction: Action
): ContentUserSigninState => {
    if (state === undefined) return ApplicationDefault.contentUserSignin;

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
