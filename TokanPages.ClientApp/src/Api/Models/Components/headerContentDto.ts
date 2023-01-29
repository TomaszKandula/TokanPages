import { LinkDto } from "./Common/linkDto";

export interface HeaderContentDto
{
    content: 
    {
        language: string;
        photo: string;
        caption: string;
        description: string;
        action: LinkDto;
    };
}
