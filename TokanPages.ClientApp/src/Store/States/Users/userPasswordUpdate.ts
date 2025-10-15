import { OperationStatus } from "../../../Shared/Enums";

export interface UserPasswordUpdateState {
    status: OperationStatus;
    response: object;
}
