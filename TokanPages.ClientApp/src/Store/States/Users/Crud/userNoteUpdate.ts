import { UpdateUserNoteResultDto } from "../../../../Api/Models";
import { OperationStatus } from "../../../../Shared/enums";

export interface UserNoteUpdateState {
    status: OperationStatus;
    response: UpdateUserNoteResultDto;
}
