import { Action, Reducer } from "redux";
import { ApplicationDefault } from "../../../Configuration";
import { UserNoteReadState } from "../../../States";
import { OperationStatus } from "../../../../Shared/enums";

import { TKnownActions, RECEIVE, CLEAR, RESPONSE } from "../../../Actions/Users/Crud/userNoteRead";

export const UserNoteRead: Reducer<UserNoteReadState> = (
    state: UserNoteReadState | undefined,
    incomingAction: Action
): UserNoteReadState => {
    if (state === undefined) return ApplicationDefault.userNoteRead;

    const action = incomingAction as TKnownActions;
    switch (action.type) {
        case CLEAR:
            return {
                status: OperationStatus.notStarted,
                response: {
                    id: "",
                    note: "",
                    createdBy: "",
                    createdAt: "",
                    modifiedBy: "",
                    modifiedAt: ""
                },
            };

        case RECEIVE:
            return {
                status: OperationStatus.inProgress,
                response: {
                    id: state.response.id,
                    note: state.response.note,
                    createdBy: state.response.createdBy,
                    createdAt: state.response.createdAt,
                    modifiedBy: state.response.modifiedBy,
                    modifiedAt: state.response.modifiedAt
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
