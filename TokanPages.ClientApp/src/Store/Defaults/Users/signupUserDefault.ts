import { ISignupUser } from "../../States/Users/signupUserState";
import { OperationStatus } from "../../../Shared/enums";

export const SignupUserDefault: ISignupUser = 
{
    operationStatus: OperationStatus.notStarted,
    attachedErrorObject: { }
}
