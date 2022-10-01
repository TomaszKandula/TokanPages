import { IUserReAuthenticate } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

export const UserReAuthenticate: IUserReAuthenticate = 
{
    operationStatus: OperationStatus.notStarted,
    attachedErrorObject: { }
}
