import { OperationStatus } from "../../../Shared/Enums";

export interface UserSigninState {
    status: OperationStatus;
    response: object;
}
