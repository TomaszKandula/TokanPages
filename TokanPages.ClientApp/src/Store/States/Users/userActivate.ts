import { OperationStatus } from "../../../Shared/enums";

export interface IUserActivate
{
    operationStatus: OperationStatus;
    attachedErrorObject: any;  
}