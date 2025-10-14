import { ActivateUserResultDto } from "../../../Api/Models";
import { OperationStatus } from "../../../Shared/Enums";

export interface UserActivateState {
    status: OperationStatus;
    response: ActivateUserResultDto;
}
