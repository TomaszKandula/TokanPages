import { ISigninUser } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

export const SigninUserDefault: ISigninUser = 
{
    operationStatus: OperationStatus.notStarted,
    attachedErrorObject: { }
}
