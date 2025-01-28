import { RemoveUserNoteResultDto } from "../../../../Api/Models";
import { OperationStatus } from "../../../../Shared/enums";

export interface UserNoteDeleteState {
    status: OperationStatus;
    response: RemoveUserNoteResultDto;
}
