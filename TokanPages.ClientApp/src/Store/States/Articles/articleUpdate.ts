import { OperationStatus } from "../../../Shared/Enums";

export interface ArticleUpdateState {
    status: OperationStatus;
    response: object;
}
