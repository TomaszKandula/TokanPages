import { IUserPasswordReset } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

export const UserPasswordReset: IUserPasswordReset = 
{
    operationStatus: OperationStatus.notStarted,
    attachedErrorObject: { }
}