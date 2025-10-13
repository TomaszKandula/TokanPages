import { OperationStatus } from "../../../Shared/Enums";
import { UserUpdateState } from "../../States";

export const UserUpdate: UserUpdateState = {
    status: OperationStatus.notStarted,
    response: {
        shouldVerifyEmail: false,
    },
};
