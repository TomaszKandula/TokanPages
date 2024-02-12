import { IconDto } from "./Common/iconDto";
import { LinkDto } from "./Common/linkDto";

export interface FooterContentDto {
    content: {
        language: string;
        terms: LinkDto;
        policy: LinkDto;
        copyright: string;
        reserved: string;
        icons: IconDto[];
    };
}
