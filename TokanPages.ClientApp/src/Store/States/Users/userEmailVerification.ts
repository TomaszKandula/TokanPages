import { OperationStatus } from "../../../Shared/enums";

export interface UserEmailVerificationState {
    status: OperationStatus;
    response: object;
}
