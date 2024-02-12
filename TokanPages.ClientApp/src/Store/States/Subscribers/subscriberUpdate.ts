import { OperationStatus } from "../../../Shared/enums";

export interface SubscriberUpdateState {
    status: OperationStatus;
    response?: any;
}
