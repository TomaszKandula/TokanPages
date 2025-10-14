import { OperationStatus } from "../../../Shared/Enums";

export interface UserSignoutState {
    userTokenStatus: OperationStatus;
    refreshTokenStatus: OperationStatus;
}
