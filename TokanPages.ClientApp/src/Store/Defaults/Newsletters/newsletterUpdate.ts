import { OperationStatus } from "../../../Shared/enums";
import { NewsletterUpdateState } from "../../States";

export const NewsletterUpdate: NewsletterUpdateState = {
    status: OperationStatus.notStarted,
    response: { },
};
