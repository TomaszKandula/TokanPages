import { OperationStatus } from "../../../Shared/enums";
import { SubscriberRemoveState } from "../../States";

export const SubscriberRemove: SubscriberRemoveState = {
    status: OperationStatus.notStarted,
    response: {},
};
