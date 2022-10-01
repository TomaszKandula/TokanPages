import { IUserPasswordUpdate } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

export const UpdateUserPasswordDefault: IUserPasswordUpdate = 
{
    operationStatus: OperationStatus.notStarted,
    attachedErrorObject: { }
}