import { OperationStatus } from "../../../Shared/enums";

export interface UserSignoutState 
{
    userTokenStatus: OperationStatus;
    refreshTokenStatus: OperationStatus;
}