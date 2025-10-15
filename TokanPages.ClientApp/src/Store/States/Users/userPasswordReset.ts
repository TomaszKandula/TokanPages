import { OperationStatus } from "../../../Shared/Enums";

export interface UserPasswordResetState {
    status: OperationStatus;
    response: object;
}
