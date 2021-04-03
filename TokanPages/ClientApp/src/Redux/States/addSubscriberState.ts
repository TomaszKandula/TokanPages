import { OperationStatuses } from "../../Shared/Enums";

export interface IAddSubscriber
{
    isAddingSubscriber: OperationStatuses;
    hasAddedSubscriber: boolean;
}
