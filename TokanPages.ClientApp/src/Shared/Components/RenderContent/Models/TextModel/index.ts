interface TextObject
{
    items: TextItem[];
}

interface TextItem
{
    id: string,
    type: string;
    value: string | RowItem[];
    prop: string;
    text: string;
}

interface RowItem
{
    column0: string;
    column1: string;
    column2: string;
}

export type { TextObject, TextItem, RowItem }
