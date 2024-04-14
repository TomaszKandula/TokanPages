import { OperationStatus } from "../../../Shared/enums";
import { NewsletterRemoveState } from "../../States";

export const NewsletterRemove: NewsletterRemoveState = {
    status: OperationStatus.notStarted,
    response: {},
};
