import { IActivateAccount } from "../../States/Users/activateAccountState";
import { OperationStatus } from "../../../Shared/enums";

export const ActivateAccountDefault: IActivateAccount = 
{
    operationStatus: OperationStatus.notStarted,
    attachedErrorObject: { }
}