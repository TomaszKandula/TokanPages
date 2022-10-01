import { IUserRemove } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

export const RemoveAccountDefault: IUserRemove = 
{
    operationStatus: OperationStatus.notStarted,
    attachedErrorObject: { }
}
