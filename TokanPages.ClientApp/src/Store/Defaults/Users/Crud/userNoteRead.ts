import { OperationStatus } from "../../../../Shared/Enums";
import { UserNoteReadState } from "../../../States";

export const UserNoteRead: UserNoteReadState = {
    status: OperationStatus.notStarted,
    response: {
        id: "",
        note: "",
        createdBy: "",
        createdAt: "",
        modifiedBy: "",
        modifiedAt: "",
    },
};
