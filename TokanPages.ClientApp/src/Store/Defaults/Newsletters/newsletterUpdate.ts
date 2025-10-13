import { OperationStatus } from "../../../Shared/Enums";
import { NewsletterUpdateState } from "../../States";

export const NewsletterUpdate: NewsletterUpdateState = {
    status: OperationStatus.notStarted,
    response: {},
};
