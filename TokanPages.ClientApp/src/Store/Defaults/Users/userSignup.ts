import { UserSignupState } from "../../States";
import { OperationStatus } from "../../../Shared/Enums";

export const UserSignup: UserSignupState = {
    status: OperationStatus.notStarted,
    response: {},
};
