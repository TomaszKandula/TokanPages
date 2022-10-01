import { OperationStatus } from "../../../Shared/enums";

export interface IArticleUpdate
{
    operationStatus: OperationStatus;
    attachedErrorObject: any;
}
