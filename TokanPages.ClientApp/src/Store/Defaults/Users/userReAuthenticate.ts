import { UserReAuthenticateState } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

export const UserReAuthenticate: UserReAuthenticateState = {
    status: OperationStatus.notStarted,
    response: {},
};
