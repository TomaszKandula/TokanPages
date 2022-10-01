import { IUserReAuthenticate } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

export const ReAuthenticateUserDefault: IUserReAuthenticate = 
{
    operationStatus: OperationStatus.notStarted,
    attachedErrorObject: { }
}
