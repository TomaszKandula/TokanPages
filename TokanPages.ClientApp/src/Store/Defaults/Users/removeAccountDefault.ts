import { IRemoveAccount } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

export const RemoveAccountDefault: IRemoveAccount = 
{
    operationStatus: OperationStatus.notStarted,
    attachedErrorObject: { }
}
