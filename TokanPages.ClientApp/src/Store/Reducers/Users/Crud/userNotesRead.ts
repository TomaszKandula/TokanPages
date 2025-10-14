import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../../Configuration";
import { UserNotesReadState } from "../../../States";
import { OperationStatus } from "../../../../Shared/Enums";

import { TKnownActions, RECEIVE, CLEAR, RESPONSE } from "../../../Actions/Users/Crud/userNotesRead";

export const UserNotesRead: Reducer<UserNotesReadState> = (
    state: UserNotesReadState | undefined,
    incomingAction: Action
): UserNotesReadState => {
    if (state === undefined) return ApplicationDefault.userNotesRead;

    const action = incomingAction as TKnownActions;
    switch (action.type) {
        case CLEAR:
            return {
                status: OperationStatus.notStarted,
                response: {
                    notes: [],
                },
            };

        case RECEIVE:
            return {
                status: OperationStatus.inProgress,
                response: {
                    notes: state.response.notes,
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
