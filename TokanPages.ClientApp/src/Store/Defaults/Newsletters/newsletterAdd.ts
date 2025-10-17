import { NewsletterAddState } from "../../States";
import { OperationStatus } from "../../../Shared/Enums";

export const NewsletterAdd: NewsletterAddState = {
    status: OperationStatus.notStarted,
    response: {},
};
