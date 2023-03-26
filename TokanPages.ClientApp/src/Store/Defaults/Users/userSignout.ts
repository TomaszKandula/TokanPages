import { OperationStatus } from "../../../Shared/enums";
import { UserSignoutState } from "../../../Store/States";

export const UserSignout: UserSignoutState = {
    status: OperationStatus.notStarted
}