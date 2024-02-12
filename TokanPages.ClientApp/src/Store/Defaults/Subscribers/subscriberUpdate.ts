import { OperationStatus } from "../../../Shared/enums";
import { SubscriberUpdateState } from "../../States";

export const SubscriberUpdate: SubscriberUpdateState = {
    status: OperationStatus.notStarted,
    response: {},
};
