import { OperationStatus } from "../../../Shared/enums";

export interface UserActivateState
{
    status: OperationStatus;
    response?: any;  
}