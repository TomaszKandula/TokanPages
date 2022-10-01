import { OperationStatus } from "../../../Shared/enums";
import { IArticleUpdate } from "../../States";

export const UpdateArticleDefault: IArticleUpdate = 
{
    operationStatus: OperationStatus.notStarted,
    attachedErrorObject: { }
}
