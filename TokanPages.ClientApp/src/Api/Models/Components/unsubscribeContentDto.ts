import { IContent } from "./Common/content";

export interface IUnsubscribeContentDto
{
    content: 
    {
        language: string;
        contentPre: IContent,
        contentPost: IContent
    };
}
