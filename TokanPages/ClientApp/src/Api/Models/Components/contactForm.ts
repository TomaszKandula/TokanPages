interface IContactForm
{
    content: IContent;
}

interface IContent
{
    caption: string;
    text: string;
    button: string;
}

export type { IContactForm }
