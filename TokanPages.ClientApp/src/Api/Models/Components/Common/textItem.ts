import { IRowItem } from "./rowItem";

export interface ITextItem
{
    id: string,
    type: string;
    value: string | IRowItem[];
    prop: string;
    text: string;
}
