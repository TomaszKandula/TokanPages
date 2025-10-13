import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../../Configuration";
import { UserNoteUpdateState } from "../../../States";
import { OperationStatus } from "../../../../Shared/Enums";

import { TKnownActions, UPDATE, CLEAR, RESPONSE } from "../../../Actions/Users/Crud/userNoteUpdate";

export const UserNoteUpdate: Reducer<UserNoteUpdateState> = (
    state: UserNoteUpdateState | undefined,
    incomingAction: Action
): UserNoteUpdateState => {
    if (state === undefined) return ApplicationDefault.userNoteUpdate;

    const action = incomingAction as TKnownActions;
    switch (action.type) {
        case CLEAR:
            return {
                status: OperationStatus.notStarted,
                response: {},
            };
        case UPDATE:
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
