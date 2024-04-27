import { NotificationData } from "../../../Api/Models";
import { OperationStatus } from "../../../Shared/enums";

export interface UserNotificationState {
    status: OperationStatus;
    response: NotificationData;
}
