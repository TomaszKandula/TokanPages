import { OperationStatus } from "../../../Shared/enums";

export interface UserUpdateState
{
    status: OperationStatus;
    response?: object;  
}
