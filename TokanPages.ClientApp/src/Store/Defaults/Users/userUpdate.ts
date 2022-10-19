import { OperationStatus } from "../../../Shared/enums";
import { IUserUpdate } from "../../States";

export const UserUpdate: IUserUpdate = 
{
    status: OperationStatus.notStarted,
    response: { }
}
