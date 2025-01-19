import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../Configuration";
import { UserNotesState } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

import { TKnownActions, RECEIVE, CLEAR, RESPONSE } from "../../Actions/Users/userNotes";

export const UserNotes: Reducer<UserNotesState> = (
    state: UserNotesState | undefined,
    incomingAction: Action
): UserNotesState => {
    if (state === undefined) return ApplicationDefault.userNotes;

    const action = incomingAction as TKnownActions;
    switch (action.type) {
        case CLEAR:
            return {
                status: OperationStatus.notStarted,
                response: {
                    notes: []
                },
            };

        case RECEIVE:
            return {
                status: OperationStatus.inProgress,
                response: {
                    notes: state.response.notes
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
