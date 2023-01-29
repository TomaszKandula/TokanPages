import { UserActivateState } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

export const UserActivate: UserActivateState = 
{
    status: OperationStatus.notStarted,
    response: { }
}