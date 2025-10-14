import { OperationStatus } from "../../../Shared/Enums";

export interface NewsletterUpdateState {
    status: OperationStatus;
    response: object;
}
