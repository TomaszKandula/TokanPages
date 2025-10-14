import { OperationStatus } from "../../../Shared/Enums";

export interface ApplicationEmailState {
    status: OperationStatus;
    response: object | string;
}
