import { IUserPasswordReset } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

export const UserPasswordReset: IUserPasswordReset = 
{
    status: OperationStatus.notStarted,
    response: { }
}