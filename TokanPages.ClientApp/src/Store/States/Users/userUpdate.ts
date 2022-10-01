import { OperationStatus } from "../../../Shared/enums";

export interface IUserUpdate
{
    operationStatus: OperationStatus;
    attachedErrorObject: any;  
}
