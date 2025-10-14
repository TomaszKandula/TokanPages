import { UserActivateState } from "../../States";
import { OperationStatus } from "../../../Shared/Enums";

export const UserActivate: UserActivateState = {
    status: OperationStatus.notStarted,
    response: {
        userId: "",
        hasBusinessLock: undefined,
    },
};
