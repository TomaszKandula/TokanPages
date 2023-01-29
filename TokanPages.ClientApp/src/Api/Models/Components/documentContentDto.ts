import { TextItemDto } from "./Common/textItemDto";

export interface DocumentContentDto
{
    content:
    {
        language: string;
        items: TextItemDto[];
    }
}
