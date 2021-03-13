export interface IHeader
{
    content: IContent;
}

interface IContent
{
    photo: string;
    caption: string;
    description: string;
    action: string;
}
