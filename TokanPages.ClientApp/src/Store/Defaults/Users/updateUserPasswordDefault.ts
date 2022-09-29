import { IUpdateUserPassword } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

export const UpdateUserPasswordDefault: IUpdateUserPassword = 
{
    operationStatus: OperationStatus.notStarted,
    attachedErrorObject: { }
}