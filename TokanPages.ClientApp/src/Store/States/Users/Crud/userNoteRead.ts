import { UserNoteResultDto } from "Api/Models";
import { OperationStatus } from "../../../../Shared/enums";

export interface UserNoteReadState {
    status: OperationStatus;
    response: UserNoteResultDto;
}
