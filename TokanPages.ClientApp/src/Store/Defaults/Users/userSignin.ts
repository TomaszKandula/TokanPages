import { UserSigninState } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

export const UserSignin: UserSigninState = {
    status: OperationStatus.notStarted,
    response: {},
};
