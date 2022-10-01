import { OperationStatus } from "../../../Shared/enums";
import { IArticleUpdate } from "../../States";

export const ArticleUpdate: IArticleUpdate = 
{
    operationStatus: OperationStatus.notStarted,
    attachedErrorObject: { }
}
