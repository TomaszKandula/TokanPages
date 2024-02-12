import { OperationStatus } from "../../../Shared/enums";

export interface ApplicationEmailState {
    status: OperationStatus;
    response?: any;
}
