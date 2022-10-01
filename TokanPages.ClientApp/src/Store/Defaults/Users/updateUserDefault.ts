import { OperationStatus } from "../../../Shared/enums";
import { IUserUpdate } from "../../States";

export const UpdateUserDefault: IUserUpdate = 
{
    operationStatus: OperationStatus.notStarted,
    attachedErrorObject: { }
}
