import { AddUserNoteResultDto } from "../../../../Api/Models";
import { OperationStatus } from "../../../../Shared/Enums";

export interface UserNoteCreateState {
    status: OperationStatus;
    response: AddUserNoteResultDto;
}
