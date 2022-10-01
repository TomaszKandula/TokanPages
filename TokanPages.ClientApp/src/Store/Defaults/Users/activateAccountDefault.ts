import { IUserActivate } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

export const UserActivate: IUserActivate = 
{
    operationStatus: OperationStatus.notStarted,
    attachedErrorObject: { }
}