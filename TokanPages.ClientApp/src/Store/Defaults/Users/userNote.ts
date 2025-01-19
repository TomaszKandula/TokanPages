import { OperationStatus } from "../../../Shared/enums";
import { UserNoteState } from "../../../Store/States";

export const UserNote: UserNoteState = {
    status: OperationStatus.notStarted,
    response: {
        note: "",
        createdBy: "",
        createdAt: "",
        modifiedBy: "",
        modifiedAt: ""
    }
} 
