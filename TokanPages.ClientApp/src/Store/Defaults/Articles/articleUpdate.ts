import { OperationStatus } from "../../../Shared/Enums";
import { ArticleUpdateState } from "../../States";

export const ArticleUpdate: ArticleUpdateState = {
    status: OperationStatus.notStarted,
    response: {},
};
