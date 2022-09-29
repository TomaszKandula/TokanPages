import { OperationStatus } from "../../../Shared/enums";

export interface IReAuthenticateUser 
{
    operationStatus: OperationStatus;
    attachedErrorObject: any;
}
