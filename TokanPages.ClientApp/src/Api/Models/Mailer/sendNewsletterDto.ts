import { SubscriberInfoDto } from "./subscriberInfoDto";

export interface SendNewsletterDto {
    subscriberInfo: SubscriberInfoDto[];
    subject: string;
    message: string;
}
