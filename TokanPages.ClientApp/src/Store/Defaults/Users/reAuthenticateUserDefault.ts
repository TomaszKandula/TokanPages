import { IReAuthenticateUser } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

export const ReAuthenticateUserDefault: IReAuthenticateUser = 
{
    operationStatus: OperationStatus.notStarted,
    attachedErrorObject: { }
}
