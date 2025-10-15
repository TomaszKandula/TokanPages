import { UserReAuthenticateState } from "../../States";
import { OperationStatus } from "../../../Shared/Enums";

export const UserReAuthenticate: UserReAuthenticateState = {
    status: OperationStatus.notStarted,
    response: {},
};
