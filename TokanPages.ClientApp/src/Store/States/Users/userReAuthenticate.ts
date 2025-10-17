import { OperationStatus } from "../../../Shared/Enums";

export interface UserReAuthenticateState {
    status: OperationStatus;
    response: object;
}
