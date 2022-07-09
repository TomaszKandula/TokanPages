import { ILink } from "./Common/link";

export interface IHeaderContentDto
{
    content: 
    {
        photo: string;
        caption: string;
        description: string;
        action: ILink;
    };
}
