import { ITextItem } from "./Common/textItem";

export interface IDocumentContentDto
{
    content:
    {
        language: string;
        items: ITextItem[];
    }
}
