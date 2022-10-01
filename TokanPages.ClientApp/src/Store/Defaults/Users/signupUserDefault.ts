import { IUserSignup } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

export const UserSignup: IUserSignup = 
{
    operationStatus: OperationStatus.notStarted,
    attachedErrorObject: { }
}
