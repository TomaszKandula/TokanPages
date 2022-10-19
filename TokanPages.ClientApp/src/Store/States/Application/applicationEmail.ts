import { OperationStatus } from "../../../Shared/enums";

export interface IApplicationEmail
{
    status: OperationStatus;
    response?: any;
}
