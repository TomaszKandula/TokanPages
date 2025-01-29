import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../../Configuration";
import { UserNoteCreateState } from "../../../States";
import { OperationStatus } from "../../../../Shared/enums";

import { TKnownActions, ADD, CLEAR, RESPONSE } from "../../../Actions/Users/Crud/userNoteCreate";

export const UserNoteCreate: Reducer<UserNoteCreateState> = (
    state: UserNoteCreateState | undefined,
    incomingAction: Action
): UserNoteCreateState => {
    if (state === undefined) return ApplicationDefault.userNoteCreate;

    const action = incomingAction as TKnownActions;
    switch (action.type) {
        case CLEAR:
            return {
                status: OperationStatus.notStarted,
                response: {
                    id: "",
                    createdAt: "",
                    createdBy: "",
                    currentNotes: 0
                },
            };
        case ADD:
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
