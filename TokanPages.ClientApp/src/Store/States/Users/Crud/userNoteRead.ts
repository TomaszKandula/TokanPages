import { UserNoteResultDto } from "../../../../Api/Models";
import { OperationStatus } from "../../../../Shared/Enums";

export interface UserNoteReadState {
    status: OperationStatus;
    response: UserNoteResultDto;
}
