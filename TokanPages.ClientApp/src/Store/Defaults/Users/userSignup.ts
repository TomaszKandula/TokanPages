import { UserSignupState } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

export const UserSignup: UserSignupState = {
    status: OperationStatus.notStarted,
    response: {},
};
