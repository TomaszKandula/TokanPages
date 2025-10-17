import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../../Configuration";
import { UserNoteDeleteState } from "../../../States";
import { OperationStatus } from "../../../../Shared/Enums";

import { TKnownActions, DELETE, CLEAR, RESPONSE } from "../../../Actions/Users/Crud/userNoteDelete";

export const UserNoteDelete: Reducer<UserNoteDeleteState> = (
    state: UserNoteDeleteState | undefined,
    incomingAction: Action
): UserNoteDeleteState => {
    if (state === undefined) return ApplicationDefault.userNoteUpdate;

    const action = incomingAction as TKnownActions;
    switch (action.type) {
        case CLEAR:
            return {
                status: OperationStatus.notStarted,
                response: {},
            };
        case DELETE:
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
