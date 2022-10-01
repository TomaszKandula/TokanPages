import { OperationStatus } from "../../../Shared/enums";

export interface IUserSignin
{
    operationStatus: OperationStatus;
    attachedErrorObject: any;
}
