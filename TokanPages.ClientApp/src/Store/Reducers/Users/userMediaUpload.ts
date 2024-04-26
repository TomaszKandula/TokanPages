import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { UserMediaUploadState } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

import { TKnownActions, UPLOAD, CLEAR, RESPONSE } from "../../Actions/Users/userMediaUpload";

export const UserMediaUpload: Reducer<UserMediaUploadState> = (
    state: UserMediaUploadState | undefined,
    incomingAction: Action
): UserMediaUploadState => {
    if (state === undefined) return ApplicationDefault.userMediaUpload;

    const action = incomingAction as TKnownActions;
    switch (action.type) {
        case CLEAR:
            return {
                handle: undefined,
                status: OperationStatus.notStarted,
                error: { },
                payload: undefined,
            };

        case UPLOAD:
            return {
                handle: state.handle,
                status: OperationStatus.inProgress,
                error: state.error,
                payload: state.payload,
            };

        case RESPONSE:
            return {
                handle: action.handle,
                status: OperationStatus.hasFinished,
                error: {},
                payload: action.payload,
            };

        default:
            return state;
    }
};
