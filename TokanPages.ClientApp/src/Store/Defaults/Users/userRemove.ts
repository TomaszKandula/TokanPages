import { IUserRemove } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

export const UserRemove: IUserRemove = 
{
    status: OperationStatus.notStarted,
    response: { }
}
