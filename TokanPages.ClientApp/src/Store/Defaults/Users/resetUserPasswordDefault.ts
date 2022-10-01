import { IUserPasswordReset } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

export const ResetUserPasswordDefault: IUserPasswordReset = 
{
    operationStatus: OperationStatus.notStarted,
    attachedErrorObject: { }
}