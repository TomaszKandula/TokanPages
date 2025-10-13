import { UpdateUserResultDto } from "../../../Api/Models";
import { OperationStatus } from "../../../Shared/Enums";

export interface UserUpdateState {
    status: OperationStatus;
    response: UpdateUserResultDto;
}
