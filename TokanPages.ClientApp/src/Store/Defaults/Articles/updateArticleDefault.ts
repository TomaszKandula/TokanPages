import { OperationStatus } from "../../../Shared/enums";
import { IUpdateArticle } from "../../States";

export const UpdateArticleDefault: IUpdateArticle = 
{
    operationStatus: OperationStatus.notStarted,
    attachedErrorObject: { }
}
