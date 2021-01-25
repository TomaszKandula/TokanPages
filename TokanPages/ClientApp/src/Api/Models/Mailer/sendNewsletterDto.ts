interface ISendNewsletterDto
{
    subscriberInfo: ISubscriberInfo[];
    subject: string;
    message: string;
}

interface ISubscriberInfo
{
    email: string;
    id: string;
}

export type { ISendNewsletterDto }
