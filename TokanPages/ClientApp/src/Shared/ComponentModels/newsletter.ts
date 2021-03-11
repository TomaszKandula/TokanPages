interface INewsletter
{
    content: IContent;
}

interface IContent
{
    caption: string;
    text: string;
    button: string;
}

export type { INewsletter }
