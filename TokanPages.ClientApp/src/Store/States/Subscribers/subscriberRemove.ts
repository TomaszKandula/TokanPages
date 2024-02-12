import { OperationStatus } from "../../../Shared/enums";

export interface SubscriberRemoveState {
    status: OperationStatus;
    response?: any;
}
