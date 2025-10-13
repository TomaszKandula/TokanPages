import { OperationStatus } from "../../../../Shared/Enums";
import { UserNoteDeleteState } from "../../../../Store/States";

export const UserNoteDelete: UserNoteDeleteState = {
    status: OperationStatus.notStarted,
    response: {},
};
