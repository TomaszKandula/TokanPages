import { OperationStatus } from "../../Shared/Enums";

export interface IAddSubscriber
{
    isAddingSubscriber: OperationStatus;
    hasAddedSubscriber: boolean;
    attachedErrorObject: any;
}
