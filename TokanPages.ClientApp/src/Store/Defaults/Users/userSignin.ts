import { UserSigninState } from "../../States";
import { OperationStatus } from "../../../Shared/Enums";

export const UserSignin: UserSigninState = {
    status: OperationStatus.notStarted,
    response: {},
};
