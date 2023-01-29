import { OperationStatus } from "../../../Shared/enums";

export interface UserPasswordResetState
{
    status: OperationStatus;
    response?: any;  
}