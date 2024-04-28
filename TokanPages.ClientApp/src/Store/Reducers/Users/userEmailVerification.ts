import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { UserEmailVerificationState } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

import { TKnownActions, VERIFY, CLEAR, RESPONSE } from "../../Actions/Users/userEmailVerification";

export const UserEmailVerification: Reducer<UserEmailVerificationState> = (
    state: UserEmailVerificationState | undefined,
    incomingAction: Action
): UserEmailVerificationState => {
    if (state === undefined) return ApplicationDefault.userEmailVerification;

    const action = incomingAction as TKnownActions;
    switch (action.type) {
        case CLEAR:
            return {
                status: OperationStatus.notStarted,
                response: {},
            };
        case VERIFY:
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
