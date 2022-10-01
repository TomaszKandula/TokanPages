import { IUserSignin } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

export const SigninUserDefault: IUserSignin = 
{
    operationStatus: OperationStatus.notStarted,
    attachedErrorObject: { }
}
