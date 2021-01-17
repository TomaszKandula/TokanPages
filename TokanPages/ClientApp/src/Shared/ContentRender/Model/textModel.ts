interface ITextObject
{
    items: ITextItem[];
}

interface ITextItem
{
    id: string,
    type: string;
    value: string;
}

export type { ITextObject, ITextItem }
