import { UserRemoveState } from "../../States";
import { OperationStatus } from "../../../Shared/Enums";

export const UserRemove: UserRemoveState = {
    status: OperationStatus.notStarted,
    response: {},
};
