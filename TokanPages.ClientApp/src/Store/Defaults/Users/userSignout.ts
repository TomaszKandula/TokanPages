import { OperationStatus } from "../../../Shared/Enums";
import { UserSignoutState } from "../../../Store/States";

export const UserSignout: UserSignoutState = {
    userTokenStatus: OperationStatus.notStarted,
    refreshTokenStatus: OperationStatus.notStarted,
};
