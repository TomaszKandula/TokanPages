interface IUpdateSubscriber
{
    content: IContent;
}

interface IContent
{
    caption: string;
    button: string;
}

export type { IUpdateSubscriber }
