import { OperationStatus } from "../../../Shared/Enums";
import { UserEmailVerificationState } from "../../../Store/States";

export const UserEmailVerification: UserEmailVerificationState = {
    status: OperationStatus.notStarted,
    response: {},
};
