import { IUserReAuthenticate } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

export const UserReAuthenticate: IUserReAuthenticate = 
{
    status: OperationStatus.notStarted,
    response: { }
}
