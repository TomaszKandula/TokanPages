import { OperationStatus } from "../../../Shared/enums";
import { UserUpdateState } from "../../States";

export const UserUpdate: UserUpdateState = {
    status: OperationStatus.notStarted,
    response: {
        shouldVerifyEmail: false,
    },
};
