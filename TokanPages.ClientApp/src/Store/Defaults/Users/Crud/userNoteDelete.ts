import { OperationStatus } from "../../../../Shared/enums";
import { UserNoteDeleteState } from "../../../../Store/States";

export const UserNoteDelete: UserNoteDeleteState = {
    status: OperationStatus.notStarted,
    response: {},
};
