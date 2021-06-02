import { ISubscriberInfoDto } from "./subscriberInfoDto";

export interface ISendNewsletterDto
{
    subscriberInfo: ISubscriberInfoDto[];
    subject: string;
    message: string;
}
