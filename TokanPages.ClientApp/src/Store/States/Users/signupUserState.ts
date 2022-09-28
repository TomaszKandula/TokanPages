import { OperationStatus } from "../../../Shared/enums";

export interface ISignupUser
{
    operationStatus: OperationStatus;
    attachedErrorObject: any;
}
