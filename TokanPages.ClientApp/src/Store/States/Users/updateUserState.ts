import { OperationStatus } from "../../../Shared/enums";

export interface IUpdateUser
{
    operationStatus: OperationStatus;
    attachedErrorObject: any;  
}
