import { OperationStatus } from "../../../Shared/enums";
import { IUpdateUser } from "../../States";

export const UpdateUserDefault: IUpdateUser = 
{
    operationStatus: OperationStatus.notStarted,
    attachedErrorObject: { }
}
