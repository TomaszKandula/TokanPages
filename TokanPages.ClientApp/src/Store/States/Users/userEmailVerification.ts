import { OperationStatus } from "../../../Shared/Enums";

export interface UserEmailVerificationState {
    status: OperationStatus;
    response: object;
}
