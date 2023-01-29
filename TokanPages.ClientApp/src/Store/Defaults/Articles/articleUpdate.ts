import { OperationStatus } from "../../../Shared/enums";
import { ArticleUpdateState } from "../../States";

export const ArticleUpdate: ArticleUpdateState = 
{
    status: OperationStatus.notStarted,
    response: { }
}
