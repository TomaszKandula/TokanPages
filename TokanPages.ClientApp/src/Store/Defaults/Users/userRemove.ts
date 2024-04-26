import { UserRemoveState } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

export const UserRemove: UserRemoveState = {
    status: OperationStatus.notStarted,
    response: { },
};
