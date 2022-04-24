import { OperationStatus } from "../../../Shared/enums";
import { IUpdateUser } from "../../States/Users/updateUserState";

export const UpdateUserDefault: IUpdateUser = 
{
    operationStatus: OperationStatus.notStarted,
    attachedErrorObject: { }
}
