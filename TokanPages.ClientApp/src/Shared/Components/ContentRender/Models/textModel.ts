interface ITextObject
{
    items: ITextItem[];
}

interface ITextItem
{
    id: string,
    type: string;
    value: string | IRowItem[];
    prop: string;
    text: string;
}

interface IRowItem
{
    column0: string;
    column1: string;
    column2: string;
}

export type { ITextObject, ITextItem, IRowItem }
