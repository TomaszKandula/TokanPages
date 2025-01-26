import { OperationStatus } from "../../../Shared/enums";
import { UserNoteState } from "../../../Store/States";

export const UserNote: UserNoteState = {
    status: OperationStatus.notStarted,
    response: {
        id: "",
        note: "",
        createdBy: "",
        createdAt: "",
        modifiedBy: "",
        modifiedAt: ""
    }
} 
