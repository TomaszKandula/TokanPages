import { IActivateAccount } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

export const ActivateAccountDefault: IActivateAccount = 
{
    operationStatus: OperationStatus.notStarted,
    attachedErrorObject: { }
}