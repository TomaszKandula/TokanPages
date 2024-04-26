import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { UserActivateState } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

import { TKnownActions, ACTIVATE, CLEAR, RESPONSE } from "../../Actions/Users/userActivate";

export const UserActivate: Reducer<UserActivateState> = (
    state: UserActivateState | undefined,
    incomingAction: Action
): UserActivateState => {
    if (state === undefined) return ApplicationDefault.userActivate;

    const action = incomingAction as TKnownActions;
    switch (action.type) {
        case CLEAR:
            return {
                status: OperationStatus.notStarted,
                response: {
                    userId: "",
                    hasBusinessLock: undefined,
                },
            };

        case ACTIVATE:
            return {
                status: OperationStatus.inProgress,
                response: {
                    userId: "",
                    hasBusinessLock: undefined,
                },
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
