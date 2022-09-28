import { ISigninUser } from "../../States/Users/signinUserState";
import { OperationStatus } from "../../../Shared/enums";

export const SigninUserDefault: ISigninUser = 
{
    operationStatus: OperationStatus.notStarted,
    attachedErrorObject: { }
}
