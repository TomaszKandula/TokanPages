import { UpdateUserResultDto } from "../../../Api/Models";
import { OperationStatus } from "../../../Shared/enums";

export interface UserUpdateState
{
    status: OperationStatus;
    response?: UpdateUserResultDto;  
}
