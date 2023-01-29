import { ContentDto } from "./Common/contentDto";

export interface UnsubscribeContentDto
{
    content: 
    {
        language: string;
        contentPre: ContentDto,
        contentPost: ContentDto
    };
}
