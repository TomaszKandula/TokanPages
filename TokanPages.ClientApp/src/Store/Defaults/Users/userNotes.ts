import { OperationStatus } from "../../../Shared/enums";
import { UserNotesState } from "../../../Store/States";

export const UserNotes: UserNotesState = {
    status: OperationStatus.notStarted,
    response: {
        notes: []
    }
} 
