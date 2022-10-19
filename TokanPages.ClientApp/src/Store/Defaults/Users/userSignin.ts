import { IUserSignin } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

export const UserSignin: IUserSignin = 
{
    status: OperationStatus.notStarted,
    response: { }
}
