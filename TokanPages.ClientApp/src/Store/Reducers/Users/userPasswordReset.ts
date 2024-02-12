import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { UserPasswordResetState } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

import { TKnownActions, RESET, CLEAR, RESPONSE } from "../../Actions/Users/userPasswordReset";

export const UserPasswordReset: Reducer<UserPasswordResetState> = (
    state: UserPasswordResetState | undefined,
    incomingAction: Action
): UserPasswordResetState => {
    if (state === undefined) return ApplicationDefault.userPasswordReset;

    const action = incomingAction as TKnownActions;
    switch (action.type) {
        case CLEAR:
            return {
                status: OperationStatus.notStarted,
                response: {},
            };
        case RESET:
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
