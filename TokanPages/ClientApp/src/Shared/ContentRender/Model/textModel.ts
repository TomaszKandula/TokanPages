interface ITextObject
{
    items: ITextItem[];
}

interface ITextItem
{
    id: string,
    type: string;
    value: string;
    prop: string;
    text: string;
}

export type { ITextObject, ITextItem }
