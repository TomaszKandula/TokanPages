import { OperationStatus } from "../../../../Shared/enums";
import { UserNoteCreateState } from "../../../../Store/States";

export const UserNoteCreate: UserNoteCreateState = {
    status: OperationStatus.notStarted,
    response: {
        currentNotes: 0,
        result: undefined
    }
}
