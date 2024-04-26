import { ActivateUserResultDto } from "Api/Models";
import { OperationStatus } from "../../../Shared/enums";

export interface UserActivateState {
    status: OperationStatus;
    response: ActivateUserResultDto;
}
