interface ICookiesPrompt
{
    content: IContent;
}

interface IContent
{
    caption: string;
    text: string;
    button: string;
}

export type { ICookiesPrompt }
