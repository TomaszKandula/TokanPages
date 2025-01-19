import { UserNoteResultDto } from "Api/Models";
import { OperationStatus } from "../../../Shared/enums";

export interface UserNoteState {
    status: OperationStatus;
    response: UserNoteResultDto;
}
