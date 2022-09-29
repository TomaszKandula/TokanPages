import { IResetUserPassword } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

export const ResetUserPasswordDefault: IResetUserPassword = 
{
    operationStatus: OperationStatus.notStarted,
    attachedErrorObject: { }
}