import { OperationStatus } from "../../../Shared/enums";

export interface NewsletterAddState {
    status: OperationStatus;
    response?: any;
}
