import { UserPasswordUpdateState } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

export const UserPasswordUpdate: UserPasswordUpdateState = {
    status: OperationStatus.notStarted,
    response: { },
};
