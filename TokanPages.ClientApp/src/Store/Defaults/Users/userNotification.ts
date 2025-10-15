import { UserNotificationState } from "../../States";
import { OperationStatus } from "../../../Shared/Enums";

export const UserNotification: UserNotificationState = {
    status: OperationStatus.notStarted,
    response: {
        userId: "",
        handler: "",
        payload: {},
    },
};
