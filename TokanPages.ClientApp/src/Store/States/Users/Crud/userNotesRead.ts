import { UserNotesResultDto } from "../../../../Api/Models";
import { OperationStatus } from "../../../../Shared/Enums";

export interface UserNotesReadState {
    status: OperationStatus;
    response: UserNotesResultDto;
}
