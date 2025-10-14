import { OperationStatus } from "../../../../Shared/Enums";
import { UserNoteCreateState } from "../../../../Store/States";

export const UserNoteCreate: UserNoteCreateState = {
    status: OperationStatus.notStarted,
    response: {
        id: "",
        createdAt: "",
        createdBy: "",
        currentNotes: 0,
        result: undefined,
    },
};
