import { AddUserNoteResultDto } from "../../../../Api/Models";
import { OperationStatus } from "../../../../Shared/enums";

export interface UserNoteCreateState {
    status: OperationStatus;
    response: AddUserNoteResultDto;
}
