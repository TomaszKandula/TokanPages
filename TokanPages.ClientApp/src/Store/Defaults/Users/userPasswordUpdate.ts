import { IUserPasswordUpdate } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

export const UserPasswordUpdate: IUserPasswordUpdate = 
{
    status: OperationStatus.notStarted,
    response: { }
}