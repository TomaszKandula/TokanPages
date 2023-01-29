import { OperationStatus } from "../../../Shared/enums";

export interface ArticleUpdateState
{
    status: OperationStatus;
    response?: any;
}
