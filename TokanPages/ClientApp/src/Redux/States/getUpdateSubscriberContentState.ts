import { IUpdateSubscriberContentDto } from "../../Api/Models";

export interface IGetUpdateSubscriberContent extends IUpdateSubscriberContentDto
{
    isLoading: boolean;
}