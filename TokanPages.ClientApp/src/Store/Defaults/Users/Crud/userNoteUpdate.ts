import { OperationStatus } from "../../../../Shared/Enums";
import { UserNoteUpdateState } from "../../../../Store/States";

export const UserNoteUpdate: UserNoteUpdateState = {
    status: OperationStatus.notStarted,
    response: {},
};
