import { OperationStatus } from "../../../Shared/enums";

export interface NewsletterUpdateState {
    status: OperationStatus;
    response?: any;
}
