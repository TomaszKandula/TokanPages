import { OperationStatus } from "../../../Shared/enums";
import { UserEmailVerificationState } from "../../../Store/States";

export const UserEmailVerification: UserEmailVerificationState = {
    status: OperationStatus.notStarted,
    response: {},
};
