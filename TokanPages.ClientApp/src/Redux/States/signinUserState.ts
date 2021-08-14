import { OperationStatus } from "../../Shared/enums";

export interface ISigninUser
{
    operationStatus: OperationStatus;
    attachedErrorObject: any;
}
