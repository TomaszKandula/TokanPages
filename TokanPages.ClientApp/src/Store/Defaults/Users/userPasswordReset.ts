import { UserPasswordResetState } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

export const UserPasswordReset: UserPasswordResetState = {
    status: OperationStatus.notStarted,
    response: { },
};
