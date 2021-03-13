interface ICookiesPrompt
{
    content: IContent;
}

interface IContent
{
    caption: string;
    text: string;
    button: string;
    days: number;
}

export type { ICookiesPrompt }
