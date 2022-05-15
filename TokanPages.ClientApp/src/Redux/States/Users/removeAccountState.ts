import { OperationStatus } from "../../../Shared/enums";

export interface IRemoveAccount
{
    operationStatus: OperationStatus;
    attachedErrorObject: any;  
}
