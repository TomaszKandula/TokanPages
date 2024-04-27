import { OperationStatus } from "../../../Shared/enums";

export interface UserSignupState {
    status: OperationStatus;
    response: object;
}
