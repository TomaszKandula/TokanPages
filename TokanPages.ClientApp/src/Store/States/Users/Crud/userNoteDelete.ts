import { RemoveUserNoteResultDto } from "../../../../Api/Models";
import { OperationStatus } from "../../../../Shared/Enums";

export interface UserNoteDeleteState {
    status: OperationStatus;
    response: RemoveUserNoteResultDto;
}
