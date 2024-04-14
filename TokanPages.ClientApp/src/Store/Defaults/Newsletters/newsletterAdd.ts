import { NewsletterAddState } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

export const NewsletterAdd: NewsletterAddState = {
    status: OperationStatus.notStarted,
    response: {},
};
