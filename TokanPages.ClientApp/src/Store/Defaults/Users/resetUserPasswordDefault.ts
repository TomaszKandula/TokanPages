import { IResetUserPassword } from "../../States/Users/resetUserPasswordState";
import { OperationStatus } from "../../../Shared/enums";

export const ResetUserPasswordDefault: IResetUserPassword = 
{
    operationStatus: OperationStatus.notStarted,
    attachedErrorObject: { }
}