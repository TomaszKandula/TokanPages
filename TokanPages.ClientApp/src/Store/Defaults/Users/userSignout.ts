import { OperationStatus } from "../../../Shared/enums";
import { UserSignoutState } from "../../../Store/States";

export const UserSignout: UserSignoutState = {
    userTokenStatus: OperationStatus.notStarted,
    refreshTokenStatus: OperationStatus.notStarted
}