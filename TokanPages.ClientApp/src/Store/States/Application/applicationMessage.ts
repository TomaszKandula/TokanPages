import { OperationStatus } from "../../../Shared/enums";

export interface IApplicationMessage
{
    status: OperationStatus;
    response?: any;
}
