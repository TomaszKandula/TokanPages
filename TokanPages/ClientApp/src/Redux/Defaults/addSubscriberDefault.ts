import { AddSubscriberEnum } from "Redux/Enums/addSubscriberEnum";
import { IAddSubscriber } from "../../Redux/States/addSubscriberState";

export const addSubscriberDefault: IAddSubscriber = 
{
    isAddingSubscriber: AddSubscriberEnum.notStarted,
    hasAddedSubscriber: false
}
