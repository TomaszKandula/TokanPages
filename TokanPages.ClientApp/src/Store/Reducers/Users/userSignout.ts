import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { UserSignoutState } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

import {
    TKnownActions,
    REVOKE_USER_TOKEN_CLEAR,
    REVOKE_REFRESH_TOKEN_CLEAR,
    REVOKE_USER_TOKEN,
    REVOKE_REFRESH_TOKEN,
    USER_TOKEN_RESPONSE,
    REFRESH_TOKEN_RESPONSE,
} from "../../Actions/Users/userSignout";

export const UserSignout: Reducer<UserSignoutState> = (
    state: UserSignoutState | undefined,
    incomingAction: Action
): UserSignoutState => {
    if (state === undefined) return ApplicationDefault.userSignout;

    const action = incomingAction as TKnownActions;
    switch (action.type) {
        case REVOKE_USER_TOKEN_CLEAR:
            return {
                userTokenStatus: OperationStatus.notStarted,
                refreshTokenStatus: state.refreshTokenStatus,
            };

        case REVOKE_REFRESH_TOKEN_CLEAR:
            return {
                userTokenStatus: state.userTokenStatus,
                refreshTokenStatus: OperationStatus.notStarted,
            };

        case REVOKE_USER_TOKEN:
            return {
                userTokenStatus: OperationStatus.inProgress,
                refreshTokenStatus: state.refreshTokenStatus,
            };

        case REVOKE_REFRESH_TOKEN:
            return {
                userTokenStatus: state.userTokenStatus,
                refreshTokenStatus: OperationStatus.inProgress,
            };

        case USER_TOKEN_RESPONSE:
            return {
                userTokenStatus: OperationStatus.hasFinished,
                refreshTokenStatus: state.refreshTokenStatus,
            };

        case REFRESH_TOKEN_RESPONSE:
            return {
                userTokenStatus: state.userTokenStatus,
                refreshTokenStatus: OperationStatus.hasFinished,
            };

        default:
            return state;
    }
};
