import { ILink } from "./Common/link";

export interface IHeaderContentDto
{
    content: 
    {
        language: string;
        photo: string;
        caption: string;
        description: string;
        action: ILink;
    };
}
