import { UserNotesResultDto } from "Api/Models";
import { OperationStatus } from "../../../Shared/enums";

export interface UserNotesState {
    status: OperationStatus;
    response: UserNotesResultDto;
}
