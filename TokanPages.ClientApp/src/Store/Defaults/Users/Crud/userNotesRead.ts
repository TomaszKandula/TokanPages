import { OperationStatus } from "../../../../Shared/enums";
import { UserNotesReadState } from "../../../States";

export const UserNotesRead: UserNotesReadState = {
    status: OperationStatus.notStarted,
    response: {
        notes: []
    }
} 
