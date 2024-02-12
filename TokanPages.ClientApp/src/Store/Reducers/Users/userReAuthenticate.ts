import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { UserReAuthenticateState } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

import { TKnownActions, REAUTHENTICATE, CLEAR, RESPONSE } from "../../Actions/Users/userReAuthenticate";

export const UserReAuthenticate: Reducer<UserReAuthenticateState> = (
    state: UserReAuthenticateState | undefined,
    incomingAction: Action
): UserReAuthenticateState => {
    if (state === undefined) return ApplicationDefault.userReAuthenticate;

    const action = incomingAction as TKnownActions;
    switch (action.type) {
        case CLEAR:
            return {
                status: OperationStatus.notStarted,
                response: {},
            };
        case REAUTHENTICATE:
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
