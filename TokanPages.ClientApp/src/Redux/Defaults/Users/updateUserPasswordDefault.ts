import { IUpdateUserPassword } from "../../States/Users/updateUserPasswordState";
import { OperationStatus } from "../../../Shared/enums";

export const UpdateUserPasswordDefault: IUpdateUserPassword = 
{
    operationStatus: OperationStatus.notStarted,
    attachedErrorObject: { }
}