import { OperationStatus } from "../../Shared/enums";

export interface IAddSubscriber
{
    isAddingSubscriber: OperationStatus;
    hasAddedSubscriber: boolean;
    attachedErrorObject: any;
}
