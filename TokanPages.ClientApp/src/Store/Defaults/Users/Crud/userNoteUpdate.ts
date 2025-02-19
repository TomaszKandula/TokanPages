import { OperationStatus } from "../../../../Shared/enums";
import { UserNoteUpdateState } from "../../../../Store/States";

export const UserNoteUpdate: UserNoteUpdateState = {
    status: OperationStatus.notStarted,
    response: {},
};
