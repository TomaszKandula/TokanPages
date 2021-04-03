import { AddSubscriberEnum } from "../../Redux/Enums/addSubscriberEnum";

export interface IAddSubscriber
{
    isAddingSubscriber: AddSubscriberEnum;
    hasAddedSubscriber: boolean;
}
