interface IUnsubscribe
{
    content: IContent;
}

interface IContent
{
    contentPre: IContentFields;
    contentPost: IContentFields;
}

interface IContentFields
{
    caption: string;
    text1: string;
    text2: string;
    text3: string;
    button: string;
}

export type { IUnsubscribe }
