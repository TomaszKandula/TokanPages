import { OperationStatus } from "../../../Shared/Enums";

export interface UserSignupState {
    status: OperationStatus;
    response: object;
}
