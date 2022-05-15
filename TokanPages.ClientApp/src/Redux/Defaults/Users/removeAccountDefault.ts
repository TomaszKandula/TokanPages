import { IRemoveAccount } from "../../States/Users/removeAccountState";
import { OperationStatus } from "../../../Shared/enums";

export const RemoveAccountDefault: IRemoveAccount = 
{
    operationStatus: OperationStatus.notStarted,
    attachedErrorObject: { }
}
