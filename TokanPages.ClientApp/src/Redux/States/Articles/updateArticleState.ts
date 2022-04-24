import { OperationStatus } from "../../../Shared/enums";

export interface IUpdateArticle
{
    operationStatus: OperationStatus;
    attachedErrorObject: any;
}
