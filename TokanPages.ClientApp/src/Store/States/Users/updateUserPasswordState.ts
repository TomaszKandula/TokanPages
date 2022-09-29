import { OperationStatus } from "../../../Shared/enums";

export interface IUpdateUserPassword
{
    operationStatus: OperationStatus;
    attachedErrorObject: any;  
}