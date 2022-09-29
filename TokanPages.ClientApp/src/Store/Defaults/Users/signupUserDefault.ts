import { ISignupUser } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

export const SignupUserDefault: ISignupUser = 
{
    operationStatus: OperationStatus.notStarted,
    attachedErrorObject: { }
}
