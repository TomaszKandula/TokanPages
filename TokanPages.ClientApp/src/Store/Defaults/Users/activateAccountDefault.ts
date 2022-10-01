import { IUserActivate } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

export const ActivateAccountDefault: IUserActivate = 
{
    operationStatus: OperationStatus.notStarted,
    attachedErrorObject: { }
}