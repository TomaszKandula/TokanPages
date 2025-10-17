import { UpdateUserNoteResultDto } from "../../../../Api/Models";
import { OperationStatus } from "../../../../Shared/Enums";

export interface UserNoteUpdateState {
    status: OperationStatus;
    response: UpdateUserNoteResultDto;
}
