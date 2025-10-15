import { UserPasswordUpdateState } from "../../States";
import { OperationStatus } from "../../../Shared/Enums";

export const UserPasswordUpdate: UserPasswordUpdateState = {
    status: OperationStatus.notStarted,
    response: {},
};
