import { OperationStatus } from "../../../../Shared/Enums";
import { UserNotesReadState } from "../../../States";

export const UserNotesRead: UserNotesReadState = {
    status: OperationStatus.notStarted,
    response: {
        notes: [],
    },
};
