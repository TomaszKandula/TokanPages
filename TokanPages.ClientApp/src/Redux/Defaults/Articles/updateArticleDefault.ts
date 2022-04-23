import { OperationStatus } from "../../../Shared/enums";
import { IUpdateArticle } from "../../../Redux/States/Articles/updateArticleState";

export const UpdateArticleDefault: IUpdateArticle = 
{
    operationStatus: OperationStatus.notStarted,
    attachedErrorObject: { }
}
