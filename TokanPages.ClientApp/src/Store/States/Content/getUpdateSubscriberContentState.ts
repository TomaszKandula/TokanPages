import { IUpdateSubscriberContentDto } from "../../../Api/Models";

export interface IContentUpdateSubscriber extends IUpdateSubscriberContentDto
{
    isLoading: boolean;
}