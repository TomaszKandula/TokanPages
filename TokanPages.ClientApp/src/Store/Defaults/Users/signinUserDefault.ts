import { IUserSignin } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

export const UserSignin: IUserSignin = 
{
    operationStatus: OperationStatus.notStarted,
    attachedErrorObject: { }
}
