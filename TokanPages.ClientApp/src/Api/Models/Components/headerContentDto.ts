import { LinkDto } from "./Common/linkDto";

export interface HeaderContentDto {
    language: string;
    photo: string;
    caption: string;
    subtitle: string;
    description: string;
    action: LinkDto;
    resume: LinkDto;
}
