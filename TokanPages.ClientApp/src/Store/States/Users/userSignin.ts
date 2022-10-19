import { OperationStatus } from "../../../Shared/enums";

export interface IUserSignin
{
    status: OperationStatus;
    response?: any;
}
