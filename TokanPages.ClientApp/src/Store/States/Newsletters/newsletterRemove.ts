import { OperationStatus } from "../../../Shared/enums";

export interface NewsletterRemoveState {
    status: OperationStatus;
    response?: any;
}
