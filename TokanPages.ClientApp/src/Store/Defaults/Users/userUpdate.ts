import { OperationStatus } from "../../../Shared/enums";
import { IUserUpdate } from "../../States";

export const UserUpdate: IUserUpdate = 
{
    operationStatus: OperationStatus.notStarted,
    attachedErrorObject: { }
}
