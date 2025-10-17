import { NotificationData } from "../../../Api/Models";
import { OperationStatus } from "../../../Shared/Enums";

export interface UserNotificationState {
    status: OperationStatus;
    response: NotificationData;
}
