import { UserPasswordResetState } from "../../States";
import { OperationStatus } from "../../../Shared/Enums";

export const UserPasswordReset: UserPasswordResetState = {
    status: OperationStatus.notStarted,
    response: {},
};
