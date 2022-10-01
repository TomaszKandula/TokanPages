import { IUserSignup } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

export const SignupUserDefault: IUserSignup = 
{
    operationStatus: OperationStatus.notStarted,
    attachedErrorObject: { }
}
