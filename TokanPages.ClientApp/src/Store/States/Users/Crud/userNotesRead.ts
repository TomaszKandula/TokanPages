import { UserNotesResultDto } from "Api/Models";
import { OperationStatus } from "../../../../Shared/enums";

export interface UserNotesReadState {
    status: OperationStatus;
    response: UserNotesResultDto;
}
