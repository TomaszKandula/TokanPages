import { OperationStatus } from "../../../Shared/enums";

export interface UserReAuthenticateState {
    status: OperationStatus;
    response: object;
}
