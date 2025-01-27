import { OperationStatus } from "../../../../Shared/enums";
import { UserNoteReadState } from "../../../States";

export const UserNoteRead: UserNoteReadState = {
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
