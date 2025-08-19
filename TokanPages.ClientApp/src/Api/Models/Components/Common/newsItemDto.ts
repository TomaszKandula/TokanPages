import { LinkDto } from "./linkDto";

export interface NewsItemDto {
    image: string;
    tags: string[];
    date: string;
    title: string;
    lead: string;
    link: LinkDto;
}
