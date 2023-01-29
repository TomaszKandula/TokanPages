import { OperationStatus } from "../../../Shared/enums";

export interface UserPasswordUpdateState
{
    status: OperationStatus;
    response?: any;  
}