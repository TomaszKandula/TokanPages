import { OperationStatus } from "../../../Shared/enums";

export interface UserSigninState {
    status: OperationStatus;
    response: object;
}
