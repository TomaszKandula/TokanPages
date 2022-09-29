import { IReAuthenticateUser } from "../../States/Users/reAuthenticateUserState";
import { OperationStatus } from "../../../Shared/enums";

export const ReAuthenticateUserDefault: IReAuthenticateUser = 
{
    operationStatus: OperationStatus.notStarted,
    attachedErrorObject: { }
}
