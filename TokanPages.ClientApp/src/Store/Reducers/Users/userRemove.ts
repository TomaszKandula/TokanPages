import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { UserRemoveState } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

import { TKnownActions, REMOVE, CLEAR, RESPONSE } from "../../Actions/Users/userRemove";

export const UserRemove: Reducer<UserRemoveState> = (
    state: UserRemoveState | undefined,
    incomingAction: Action
): UserRemoveState => {
    if (state === undefined) return ApplicationDefault.userRemove;

    const action = incomingAction as TKnownActions;
    switch (action.type) {
        case CLEAR:
            return {
                status: OperationStatus.notStarted,
                response: { },
            };
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
